function UpdateCircularProgressBars($){
    $('.circular-progress-bar-js').each(function(index){
        var bar = $(this);
        var current = bar.data('current');
        var progress = bar.data('progress');
        var progressDegLeft = 0;
        var progressDegRight = 0;
        if (progress <= 50){
            progressDegLeft = (180*progress)/50;
        }
        else{
            progressDegLeft = 180;
            progressDegRight = (180*(progress-50))/50;
        }
        var leftBar = bar.find('.circular-progress-left .circular-progress-bar');
        var rightBar = bar.find('.circular-progress-right .circular-progress-bar');    
        if(progress > current){
            if(progress <= 50){ // 25 -> 40
                leftBar.css('transition-delay', '0s');
                rightBar.css('transition-delay', '0s');
            }
            else{
                if(current <= 50){ // 25 -> 75
                    leftBar.css('transition-delay', '0s');
                    rightBar.css('transition-delay', '1.5s');
                }
                else{ // 55 -> 75
                    leftBar.css('transition-delay', '0s');
                    rightBar.css('transition-delay', '0s');    
                }
            }
        }
        else if(progress < current){
            if(progress <= 50){ 
                if(current <= 50){ // 45 -> 25
                    leftBar.css('transition-delay', '0s');
                    rightBar.css('transition-delay', '0s');
                }
                else{ // 75 -> 25
                    leftBar.css('transition-delay', '1.5s');
                    rightBar.css('transition-delay', '0s');
                }
            }
            else{ // 75 -> 60
                leftBar.css('transition-delay', '0s');
                rightBar.css('transition-delay', '0s');
            }
        }
        leftBar.css('transform', 'rotate('+progressDegLeft+'deg)');
        rightBar.css('transform', 'rotate('+progressDegRight+'deg)');
        bar.attr('data-current', progress);
    });    
};

/* Project specific Javascript goes here. */
(function ($) {
    var $document = $(document).ready(function() {
        var $window = $(window);
        var $body = $('body');

        // Double slider
        var slider_2 = $("#slider-range-entryfee");
        var entryfee_min = $('#entryfee-min');
        var entryfee_max = $('#entryfee-max');
        var min_value = $('#min-value');
        var max_value = $('#max-value');
        function UpdateValues()
        {
            min_value.text('$ '+ entryfee_min.val());
            max_value.text('$ '+ entryfee_max.val());
            min_value.css('left', handle_min.css('left'));
            max_value.css('left', handle_max.css('left'));
        }
        slider_2.slider({
            range: true,
            min: 0,
            max: 1000,
            values: [ 0, 1000 ],
            slide: function( event, ui ) {
                entryfee_min.val( ui.values[0]);
                entryfee_max.val( ui.values[1]);
                min_value.text('$ '+ ui.values[0]);
                max_value.text('$ '+ ui.values[1]);
                UpdateValues();
            }
        });
        var handle_min = slider_2.find('.ui-slider-handle').eq(0);
        var handle_max = slider_2.find('.ui-slider-handle').eq(1);
        UpdateValues();
        $('.ui-slider-handle').addClass('custom-slider-handle');
        // -----------------------------------------------------------

        $body.on('mouseenter', '.contest-cap', function(event){
//             var self = $(this);
//             self.find('.side.front')
//                 .toggleClass('activate-flip-back');            
        });
        $body.on('mouseleave', '.contest-cap', function(event){
//             var self = $(this);            
//             self.find('.side.back')
//                 .toggleClass('activate-flip-back');
        });        

        UpdateCircularProgressBars($);
    });
})(jQuery);

