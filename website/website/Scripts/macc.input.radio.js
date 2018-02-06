/* Project specific Javascript goes here. */
(function ($) {
    $(document).ready(function() {        
        var body = $('body');

        body.on('click', '.radio-btn-js', function(){
            var self = $(this);            
            var targetSelector = self.data('target');
            var groupSelector = self.data('group');
            var value = self.data('value');
            var target = $(targetSelector);
            $(groupSelector).removeClass('active');
            self.addClass('active');
            target.val(value);
        });
    });
})(jQuery);

