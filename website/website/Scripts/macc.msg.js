/* Project specific Javascript goes here. */
// depends on toastr.js
(function ($) {
    $(document).ready(function() {

        var msgs = $('.show-message-js');
        toastr.options.positionClass = "toast-top-left";       
        //toastr.options.newestOnTop = false;        
        toastr.options.extendedTimeOut = 10000;
        toastr.options.timeOut = 10000;
        msgs.each(function(index){
            var self = $(this);
            var time = parseInt(self.data('time'));
             toastr.options.extendedTimeOut = time;
             toastr.options.timeOut = time;
            if (self.data('type') == 'success'){
                toastr.success(self.text());                 
            }
            else if (self.data('type') == 'error'){
                toastr.error(self.text());    
            }     
            else if (self.data('type') == 'warning'){
                toastr.warning(self.text());    
            }
            else if (self.data('type') == 'info'){
                toastr.info(self.text());    
            }            
        });  

    });
})(jQuery);

