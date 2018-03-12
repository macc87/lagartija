function UpdateCircularProgressBar($, bar){
    var progress = bar.data('progress');    
    var currentProgress = bar.data('current');
    var transitionTime = bar.data('time');
    var handle = bar.find('.percent-handle');
    var handleRotation = (progress * 2.7) - 45;    
    bar.find('.data-progress').text(progress);
    var leftBar = bar.find('.circular-progress-left .circular-progress-bar');
    var rightBar = bar.find('.circular-progress-right .circular-progress-bar');                
    var progressDegLeft = 0;
    var progressDegRight = 0;
    var progressPercentLeft = 33.33;
    var progressPercentRight = 66.66;
    if (progress <= 33.33){
        progressDegLeft = (90*progress)/33.33;            
        progressPercentLeft = progress;
        progressPercentRight = 0;
    }
    else{
        progressDegLeft = 91;            
        progressDegRight = (180*(progress-33.33))/66.66;                    
        progressPercentRight = progress - 33.33;
    }

    if(currentProgress <= 33.33 && progress <= 33.33){
        leftBar.css('transition-duration', transitionTime+'ms');
        rightBar.css('transition-duration', '0ms');                    
        leftBar.css('transition-delay', '0ms');
        rightBar.css('transition-delay', '0ms');                    
    }
    else if(currentProgress > 33.33 && progress > 33.33){
        leftBar.css('transition-duration', '0ms');
        rightBar.css('transition-duration', transitionTime +'ms');                    
        leftBar.css('transition-delay', '0ms');
        rightBar.css('transition-delay', '0ms');                    
    }
    else {
        if(progress < currentProgress){        
            if(progress == 0){
                leftBar.css('transition-duration', (transitionTime * 0.33) +'ms');
                rightBar.css('transition-duration', (transitionTime * 0.66) +'ms');                    
                leftBar.css('transition-delay', (transitionTime * 0.66) +'ms');                    
                rightBar.css('transition-delay', 0 +'ms');                        
            }                
            else{
                var timeLeft = (transitionTime * (33.33 - progress) / (currentProgress - progress));
                var timeRight = (transitionTime * (currentProgress - 33.33) / (currentProgress - progress));
                leftBar.css('transition-duration',  timeLeft+'ms');
                rightBar.css('transition-duration', timeRight+'ms');                    
                leftBar.css('transition-delay', timeRight+'0ms');
                rightBar.css('transition-delay', '0ms');                        
            }
        }
        else{            
            var timeLeft = (transitionTime * (33.33 - currentProgress) / (progress - currentProgress));
            var timeRight = (transitionTime * (progress - 33.33) / (progress - currentProgress));
            leftBar.css('transition-duration',  timeLeft+'ms');
            rightBar.css('transition-duration', timeRight+'ms');                    
            leftBar.css('transition-delay', '0ms');
            rightBar.css('transition-delay', timeLeft+'ms');                                    
        }       
    }   
    
    
    handle.css('transition-duration', transitionTime+'ms');    
    handle.find('div').css('transition-duration', transitionTime+'ms');   
       
     
    if(progress >= 33.33){
        //leftBar.css('transition-delay', '0ms');
        //rightBar.css('transition-delay', (transitionTime * progressPercentLeft / progress - transitionTime/4) +'ms');                        
    }    
    
    leftBar.css('transform', 'rotate('+progressDegLeft+'deg)');
    rightBar.css('transform', 'rotate('+progressDegRight+'deg)');                
    handle.css('transform', 'rotate('+handleRotation+'deg)');
    handle.find('div').css('transform', 'rotate('+(-1*handleRotation)+'deg)');
    bar.data('current', progress);
};

function UpdateCircularProgressBars($){
    $('.radial-progress-objetive.circular-progress-bar-js').each(function(index){
        var bar = $(this);             
        UpdateCircularProgressBar($, bar);           
    });    
};

function UpdateCapsCircularProgressBars($){
    $('.radial-progress-cap.circular-progress-bar-js').each(function(index){
        var bar = $(this);             
        UpdateCircularProgressBar($, bar);           
    });    
};

function UpdateTotalScoreBar($, bar){
    var contestants = bar.data('contestants');
    var position = bar.data('position');
    var winPosition = bar.data('win-position');
    var prize = bar.data('prize');
    var points = bar.data('points');
    var percent = bar.data('percent');
    var transitionTime = bar.data('time');    

    bar.find('.data-contestants').text(contestants);
    bar.find('.data-position').text(position);
    bar.find('.data-prize').text(prize);
    bar.find('.data-points').text(points);
    var circularBar = bar.find('.circular-progress-bar-js');    
    circularBar.data('progress', percent);
    circularBar.data('time', transitionTime);    
    UpdateCircularProgressBar($, circularBar);

    var widthPercent = 100 - 100 * position / contestants;
    var winWidthPercent = 100 - 100 * winPosition / contestants;

    bar.find('.win-separator').css('left', winWidthPercent+'%');
    bar.find('.win-separator').css('transition-duration', transitionTime+'ms');
    bar.find('.win-marker').css('left', winWidthPercent+'%');
    bar.find('.win-marker').css('transition-duration', transitionTime+'ms');
    bar.find('.progress-bar').css('width', widthPercent+'%');
    bar.find('.progress-bar').css('transition-duration', transitionTime+'ms');
    bar.find('.position-marker').css('left', widthPercent+'%');
    bar.find('.position-marker').css('transition-duration', transitionTime+'ms');
    bar.find('.prize').css('left', widthPercent+'%');
    bar.find('.prize').css('transition-duration', transitionTime+'ms');
    bar.find('.prize-marker').css('left', widthPercent+'%');
    bar.find('.prize-marker').css('transition-duration', transitionTime+'ms');
    bar.find('.position-info').css('left', widthPercent+'%');
    bar.find('.position-info').css('transition-duration', transitionTime+'ms');
};

function UpdateTotalScoreBars($){
    $('.score-js').each(function(index){
        var bar = $(this);
        UpdateTotalScoreBar($, bar);
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

        var progress = 10;
        var position = 3732;
        var points = 0;
        var timeHandle = 0;
        var data = [
            {'progress': 10, 'position': 3700, 'points': 0, 'prize': '$0'},
            {'progress': 50, 'position': 3700, 'points': 1, 'prize': '$0'},
            {'progress': 70, 'position': 3700, 'points': 2, 'prize': '$0'},
            {'progress': 100, 'position': 2400, 'points': 3, 'prize': '$0'},
            {'progress': 0, 'position': 2400, 'points': 4, 'prize': '$0'},
            {'progress': 35, 'position': 2400, 'points': 5, 'prize': '$0'},
            {'progress': 40, 'position': 1300, 'points': 6, 'prize': '$0'},
            {'progress': 20, 'position': 900, 'points': 7, 'prize': '$1000'},
            {'progress': 80, 'position': 2000, 'points': 8, 'prize': '$0'},
            {'progress': 90, 'position': 2000, 'points': 9, 'prize': '$0'},
            {'progress': 100, 'position': 1500, 'points': 10, 'prize': '$1500'},
            {'progress': 50, 'position': 10, 'points': 11, 'prize': '$2500'},
        ]
        var pB = $('.score-js');
        t = 0;
        function Tick(){
            if(t >= data.length )
            {
                // clearInterval(timeHandle);
                t = 0;
            }
            else{                
                pB.data('percent', data[t]['progress']);
                pB.data('position', data[t]['position']);
                pB.data('points', data[t]['points']);
                pB.data('prize', data[t]['prize']);
                UpdateTotalScoreBars($);                                
                t += 1;
            }
        };
        timeHandle = setInterval(Tick, 1200); 

        UpdateTotalScoreBars($);
        UpdateCapsCircularProgressBars($);
    });
})(jQuery);

