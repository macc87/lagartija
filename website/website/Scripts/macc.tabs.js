/* Project specific Javascript goes here. */
(function ($) {
    $(document).ready(function() {        
        var body = $('body');

        body.on('click', '.tab', function(){
            var self = $(this);
            var container = self.parents('.tab-view');
            var target = self.data('target');
            container.find('.tab-content').removeClass('active');
            container.find('.tab').removeClass('active');
            container.find(target).addClass('active');
            self.addClass('active');
        });
    });
})(jQuery);

