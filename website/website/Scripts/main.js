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
            
                var timeLeft = (transitionTime * (33.33 - progress) / (currentProgress - progress));
                var timeRight = (transitionTime * (currentProgress - 33.33) / (currentProgress - progress));
                leftBar.css('transition-duration',  timeLeft+'ms');
                rightBar.css('transition-duration', timeRight+'ms');                    
                leftBar.css('transition-delay', timeRight+'0ms');
                rightBar.css('transition-delay', '0ms');                        
            
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

function UpdateTargetScoreBar($, bar){
    var events = bar.data('events');
    var completed = bar.data('completed-events');    
    var transitionTime = bar.data('time');        
    var points = parseInt(completed / events);
    var percent = parseInt(100 * (completed % events) / events);
    var max_events = (parseInt(completed / events) + 2) * events;
    
    var eMarkers = bar.find('.event-marker').addClass('hidden');
    for(var i = 1; i*events < max_events; i += 1)
    {
        var e = i*events;
        var marker = 0;
        if(i <= eMarkers.length){
            marker = $(eMarkers[i-1]);
            marker.removeClass('hidden');
            marker.find('.events-info').text(e);
            if(completed >= e){
                marker.addClass('completed');
            }
            else {
                marker.removeClass('completed');
            }
            var leftPercent = 100 * (e / max_events);
            var leftOffset = bar.width() * (e / max_events);
            // marker.css('left', leftOffset + 'px');        
            marker.css('left', leftPercent + '%');        
        }
    }

    var progressPercent = 100 * completed / max_events;
    var completedMarker = bar.find('.events-completed-marker');
    bar.find('.data-completed-events').text(completed);    
    completedMarker.css('left', progressPercent + '%');   
    completedMarker.css('transition-duration', transitionTime+'ms');
    bar.find('.progress-bar').css('width', progressPercent+'%');
    bar.find('.progress-bar').css('transition-duration', transitionTime+'ms');

    bar.find('.data-points').text(points);
    var circularBar = bar.find('.circular-progress-bar-js');    
    circularBar.data('progress', percent);
    circularBar.data('time', transitionTime);    
    UpdateCircularProgressBar($, circularBar);
};

function UpdateTargetScoreBars($){
    $('.target-score-js').each(function(index){
        var bar = $(this);
        UpdateTargetScoreBar($, bar);
    });
};

var timeHandle = 0;
function SimContest($){        
    var dataTotal = [
        {'progress': 0, 'position': 100, 'points': 0, 'prize': '$0'},        
        {'progress': 53, 'position': 60, 'points': 1, 'prize': '$0'},        
        {'progress': 90, 'position': 40, 'points': 2, 'prize': '$100'},        
        {'progress': 95, 'position': 45, 'points': 2, 'prize': '$50'},        
        {'progress': 10, 'position': 3, 'points': 4, 'prize': '$500'},        
        {'progress': 30, 'position': 47, 'points': 4, 'prize': '$200'},        
        {'progress': 30, 'position': 60, 'points': 4, 'prize': '$0'},        
        {'progress': 30, 'position': 80, 'points': 4, 'prize': '$0'},        
    ];
    var dataHits = [
        {'events': 9, 'completed-events': 0},        
        {'events': 9, 'completed-events': 4},        
        {'events': 9, 'completed-events': 11},        
        {'events': 9, 'completed-events': 17},        
        {'events': 9, 'completed-events': 21},        
        {'events': 9, 'completed-events': 22},        
        {'events': 9, 'completed-events': 22},        
        {'events': 9, 'completed-events': 22},        
    ];
    var dataDoubles = [
        {'events': 3, 'completed-events': 0},        
        {'events': 3, 'completed-events': 5},        
        {'events': 3, 'completed-events': 5},        
        {'events': 3, 'completed-events': 5},        
        {'events': 3, 'completed-events': 7},        
        {'events': 3, 'completed-events': 8},        
        {'events': 3, 'completed-events': 8},        
        {'events': 3, 'completed-events': 8},        
    ];
    var totalScore = $('#total-bar');
    var hitsScore = $('#hits-bar');
    var doublesScore = $('#doubles-bar');
    t = 0;
    function Tick(){
        if(t >= dataTotal.length )
        {
            // clearInterval(timeHandle);
            t = 0;
        }
        else{                
            hitsScore.data('events', dataHits[t]['events']);
            hitsScore.data('completed-events', dataHits[t]['completed-events']);            
            UpdateTargetScoreBar($, hitsScore);                                

            doublesScore.data('events', dataDoubles[t]['events']);
            doublesScore.data('completed-events', dataDoubles[t]['completed-events']);            
            UpdateTargetScoreBar($, doublesScore);                                

            totalScore.data('percent', dataTotal[t]['progress']);
            totalScore.data('position', dataTotal[t]['position']);
            totalScore.data('points', dataTotal[t]['points']);
            totalScore.data('prize', dataTotal[t]['prize']);
            UpdateTotalScoreBar($, totalScore);             
                                  
            t += 1;
        }
    };
    clearInterval(timeHandle);
    timeHandle = setInterval(Tick, 2000); 
};

var timeHandle = 0;
function SimContestLive($){    
    var totalScore = $('#total-bar-1');    
    var totalData = [
        {'progress': 0, 'position': 100, 'points': 0, 'prize': '$0'},        
        {'progress': 53, 'position': 60, 'points': 1, 'prize': '$0'},        
        {'progress': 90, 'position': 40, 'points': 2, 'prize': '$100'},        
        {'progress': 95, 'position': 45, 'points': 2, 'prize': '$50'},        
        {'progress': 10, 'position': 3, 'points': 4, 'prize': '$500'},        
        {'progress': 30, 'position': 47, 'points': 4, 'prize': '$200'},        
        {'progress': 30, 'position': 60, 'points': 4, 'prize': '$0'},        
        {'progress': 30, 'position': 80, 'points': 4, 'prize': '$0'},        
    ];
    var hitsScore = $('#hits-bar-1');
    var hitsData = [
        {'events': 5, 'completed-events': 0},        
        {'events': 5, 'completed-events': 4},        
        {'events': 5, 'completed-events': 11},        
        {'events': 5, 'completed-events': 17},        
        {'events': 5, 'completed-events': 21},        
        {'events': 5, 'completed-events': 22},        
        {'events': 5, 'completed-events': 22},        
        {'events': 5, 'completed-events': 22},        
    ];
    var doublesScore = $('#doubles-bar-1');
    var doublesData = [
        {'events': 2, 'completed-events': 0},        
        {'events': 2, 'completed-events': 5},        
        {'events': 2, 'completed-events': 5},        
        {'events': 2, 'completed-events': 5},        
        {'events': 2, 'completed-events': 7},        
        {'events': 2, 'completed-events': 8},        
        {'events': 2, 'completed-events': 8},        
        {'events': 2, 'completed-events': 8},        
    ];
    var triplesScore = $('#triples-bar-1');
    var triplesData = [
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 1},        
        {'events': 1, 'completed-events': 1},        
        {'events': 1, 'completed-events': 2},        
        {'events': 1, 'completed-events': 2},        
        {'events': 1, 'completed-events': 2},        
    ];
    var rScore = $('#r-bar-1');
    var rData = [
        {'events': 4, 'completed-events': 0},        
        {'events': 4, 'completed-events': 0},        
        {'events': 4, 'completed-events': 0},        
        {'events': 4, 'completed-events': 1},        
        {'events': 4, 'completed-events': 1},        
        {'events': 4, 'completed-events': 2},        
        {'events': 4, 'completed-events': 2},        
        {'events': 4, 'completed-events': 2},        
    ];
    var hrScore = $('#hr-bar-1');
    var hrData = [
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 1},        
        {'events': 1, 'completed-events': 1},        
        {'events': 1, 'completed-events': 2},        
        {'events': 1, 'completed-events': 2},        
        {'events': 1, 'completed-events': 3},        
        {'events': 1, 'completed-events': 3},        
        {'events': 1, 'completed-events': 3},        
    ];
    var rbiScore = $('#rbi-bar-1');
    var rbiData = [
        {'events': 4, 'completed-events': 0},        
        {'events': 4, 'completed-events': 1},        
        {'events': 4, 'completed-events': 1},        
        {'events': 4, 'completed-events': 2},        
        {'events': 4, 'completed-events': 2},        
        {'events': 4, 'completed-events': 4},        
        {'events': 4, 'completed-events': 4},        
        {'events': 4, 'completed-events': 4},        
    ];
    var bbScore = $('#bb-bar-1');
    var bbData = [
        {'events': 3, 'completed-events': 0},        
        {'events': 3, 'completed-events': 1},        
        {'events': 3, 'completed-events': 2},        
        {'events': 3, 'completed-events': 3},        
        {'events': 3, 'completed-events': 3},        
        {'events': 3, 'completed-events': 4},        
        {'events': 3, 'completed-events': 5},        
        {'events': 3, 'completed-events': 5},        
    ];
    var sbScore = $('#sb-bar-1');
    var sbData = [
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 0},        
        {'events': 1, 'completed-events': 1},        
        {'events': 1, 'completed-events': 1},        
        {'events': 1, 'completed-events': 2},        
        {'events': 1, 'completed-events': 2},        
    ];
    var oScore = $('#o-bar-1');
    var oData = [
        {'events': 9, 'completed-events': 0},        
        {'events': 9, 'completed-events': 4},        
        {'events': 9, 'completed-events': 4},        
        {'events': 9, 'completed-events': 9},        
        {'events': 9, 'completed-events': 12},        
        {'events': 9, 'completed-events': 12},        
        {'events': 9, 'completed-events': 14},        
        {'events': 9, 'completed-events': 14},        
    ];
    var kScore = $('#k-bar-1');
    var kData = [
        {'events': 4, 'completed-events': 0},        
        {'events': 4, 'completed-events': 2},        
        {'events': 4, 'completed-events': 3},        
        {'events': 4, 'completed-events': 3},        
        {'events': 4, 'completed-events': 6},        
        {'events': 4, 'completed-events': 6},        
        {'events': 4, 'completed-events': 7},        
        {'events': 4, 'completed-events': 9},        
    ];
    t = 0;
    function Tick(){
        if(t >= totalData.length )
        {
            // clearInterval(timeHandle);
            t = 0;
        }
        else{                
            hitsScore.data('events', hitsData[t]['events']);
            hitsScore.data('completed-events', hitsData[t]['completed-events']);            
            UpdateTargetScoreBar($, hitsScore);                                

            doublesScore.data('events', doublesData[t]['events']);
            doublesScore.data('completed-events', doublesData[t]['completed-events']);            
            UpdateTargetScoreBar($, doublesScore);                                

            triplesScore.data('events', triplesData[t]['events']);
            triplesScore.data('completed-events', triplesData[t]['completed-events']);            
            UpdateTargetScoreBar($, triplesScore);                                

            rScore.data('events', rData[t]['events']);
            rScore.data('completed-events', rData[t]['completed-events']);            
            UpdateTargetScoreBar($, rScore);                                

            hrScore.data('events', hrData[t]['events']);
            hrScore.data('completed-events', hrData[t]['completed-events']);            
            UpdateTargetScoreBar($, hrScore);                                

            rbiScore.data('events', rbiData[t]['events']);
            rbiScore.data('completed-events', rbiData[t]['completed-events']);            
            UpdateTargetScoreBar($, rbiScore);                                

            bbScore.data('events', bbData[t]['events']);
            bbScore.data('completed-events', bbData[t]['completed-events']);            
            UpdateTargetScoreBar($, bbScore);                                

            sbScore.data('events', sbData[t]['events']);
            sbScore.data('completed-events', sbData[t]['completed-events']);            
            UpdateTargetScoreBar($, sbScore);                                

            oScore.data('events', oData[t]['events']);
            oScore.data('completed-events', oData[t]['completed-events']);            
            UpdateTargetScoreBar($, oScore);                                

            kScore.data('events', kData[t]['events']);
            kScore.data('completed-events', kData[t]['completed-events']);            
            UpdateTargetScoreBar($, kScore);                                

            totalScore.data('percent', totalData[t]['progress']);
            totalScore.data('position', totalData[t]['position']);
            totalScore.data('points', totalData[t]['points']);
            totalScore.data('prize', totalData[t]['prize']);
            UpdateTotalScoreBar($, totalScore);             
                                  
            t += 1;
        }
    };
    clearInterval(timeHandle);
    timeHandle = setInterval(Tick, 2000); 
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

        UpdateTargetScoreBars($);

        $('.nicescroll-js').niceScroll();

        SimContestLive($);
    });
})(jQuery);

