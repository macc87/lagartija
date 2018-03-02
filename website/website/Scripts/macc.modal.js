(function($) {
    $(document).ready(function() {
        var $body = $('body');
        var modal = $('#empty-modal-js');
        
        $body.on('click', '.show-modal-on-click-js', function(event) {
            event.preventDefault();
            $($(this).data('target')).modal();
        });
        
        // Cargar contenido en modal ------------------------
        $body.on('click', '.load-on-modal-js', function(event) {
            event.preventDefault();
            var self = $(this);
            modal.modal('hide');
            GetPage({
                url: self.attr('href'),
                success: function(response) {
                    modal.find('.modal-content').html(response);
                    modal.modal('show');
                }
            });
        });
        // --------------------------------------------------
        
        // Submit por Ajax cuando el form esta en un modal
        $('.ajax-form-js').on('submit', 'form', function(event) {
            var form = $(this);
            modal.modal('hide');
            if (!form.hasClass('no-ajax-form')) 
            {
                LoaderStartAnimation();
                event.preventDefault();
                AjaxFormSubmit(
                form, 
                form.parents('.modal-body'), 
                function(response) {
                    LoaderStopAnimation();
                    location.reload();
                }, 
                function(response) {
                    LoaderStopAnimation();
                    modal.modal('show');
                    $('.ajax-select-trigger-js').each(function(index) {
                        Change($(this), true);
                    });
                    $('input[type=date]').datepicker({
                        format: 'yyyy-mm-dd',
                        autoclose: true,
                        language: 'es',
                        orientation: 'bottom',
                    });
                }
                );
            }
        
        });
        // ------------------------------------------------------------
    
    });
})(jQuery);
