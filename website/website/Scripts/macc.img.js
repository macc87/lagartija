
var imgbackground = null;

(function ($) {
    $(document).ready(function() {
        //var modal = $('#modal-js');
        var $body = $('body');

        /*
        <div class="background-image-js"
             data-src="{{ asset(hotel.cardImage) }}"
             data-size="cover"
             data-position="center center">
        </div>
        */

        imgbackground = function(){
            // Background Image         
            $('.background-image-js').each(function(index){
                var self = $(this);
                self.css({
                    'background': "url('"+self.data('src')+"')",
                    'background-size': self.data('size'),
                    'background-position': self.data('position'),
                    'background-repeat': 'no-repeat',
                    'background-color': self.data('color'),
                });
            });        
            // ---------------------------------------------------            
        };

        imgbackground();

    });
})(jQuery);

