
var GetPage = null;
var RunAjaxSuccessFunctions = function(){
    if(imgbackground)    
        imgbackground();
    if(initscrollableslider)    
        initscrollableslider();   
};

(function ($) {
    $(document).ready(function() {
        //var modal = $('#modal-js');
        var $body = $('body');

        // [Pedir pagina por ajax] -------------
        // opt
        /// url
        /// data
        /// success (callback(response))
        GetPage = function (opt) {
            var url = opt.url;
            var data = opt.data;
            var success = opt.success;
            $.ajax({
                url: url,
                dataType: "html",
                type: 'get',
                data: data,
                async: true,
                success: function(response) {
                    if(success){
                        success(response);
                        RunAjaxSuccessFunctions();
                    }
                },
                error: function(response) {
                    console.log(response);
                },
            });
        };
        // -------------------------------------------

        // Accion y Recarga--------------------------------------------------
        $body.on('click', '.ajax-on-click-js', function (event) {
            event.preventDefault();
            //modal.modal('hide');
            var self = $(this);
            GetPage({
                url: self.attr('href'),
                success: function(response){
                    location.reload();
                }});
        });
        // --------------------------------------------------

        $('.ajax-on-load').each(function (i, element) {
            var container = $(element);
            GetPage({
                url: container.data('url'),
                success: function(response){
                    container.html(response);
                }
            })
        });
    });
})(jQuery);

