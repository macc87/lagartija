function UpdateCircularProgressBar($, bar){
    var progress = bar.data('progress');        
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
    leftBar.css('transition-duration', (1200 * progressPercentLeft / progress) +'ms');
    rightBar.css('transition-duration', (1200 * progressPercentRight / progress) +'ms');    
    leftBar.css('transition-delay', '0ms');
    rightBar.css('transition-delay', (1200 * progressPercentLeft / progress) +'ms');                
    leftBar.css('transform', 'rotate('+progressDegLeft+'deg)');
    rightBar.css('transform', 'rotate('+progressDegRight+'deg)');        
    bar.attr('data-current', progress);
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

    bar.find('.data-contestants').text(contestants);
    bar.find('.data-position').text(position);
    bar.find('.data-prize').text(prize);
    bar.find('.data-points').text(points);
    var circularBar = bar.find('.circular-progress-bar-js');    
    circularBar.attr('data-progress', percent);
    UpdateCircularProgressBar($, circularBar);

    var widthPercent = 100 * position / contestants;
    var winWidthPercent = 100 * winPosition / contestants;

    bar.find('.win-separator').css('left', winWidthPercent+'%');
    bar.find('.win-marker').css('left', winWidthPercent+'%');    
    bar.find('.progress-bar').css('width', widthPercent+'%');    
    bar.find('.position-marker').css('left', widthPercent+'%');
    bar.find('.prize').css('left', widthPercent+'%');
    bar.find('.prize-marker').css('left', widthPercent+'%');
    bar.find('.position-info').css('left', widthPercent+'%');
};

function UpdateTotalScoreBars($){
    $('.total-score-js').each(function(index){
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

        UpdateTotalScoreBars($);
        UpdateCapsCircularProgressBars($);
    });
})(jQuery);

