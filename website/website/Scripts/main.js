/* Project specific Javascript goes here. */
(function ($) {
    var $document = $(document).ready(function() {
        var $window = $(window);

        // Double slider
        var slider_2 = $("#slider-range-entryfee");
        var entryfee_min = $('#entryfee_min');
        var entryfee_max = $('#entryfee_max');
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
        $('.ui-slider-handle').addClass('fa fa-shield');
        // -----------------------------------------------------------
    });
})(jQuery);

