
var initscrollableslider = null;

(function ($) {
    $(document).ready(function() {
        //var modal = $('#modal-js');
        var $body = $('body');

        // ScrollableSlider ------------------------------------
        /// container
        var ScrollableSlider = function (params) {
            if(params.container.length == 0)
            {
                this.isUndefined = true;
                return $(this);
            }
            if(params.container.length > 1) {
                var ret = params.container.map(function(){
                    var p = params;
                    p.container = $(this);
                    return new ScrollableSlider(p);
                });
                return $(ret);
            }
            this.isUndefined = false;
            this.container = params.container;
            //this.leftControl = this.container
            //    .find('.scroll-indicator.left')
            //    .data('dir', -1);
            //this.rightControl = this.container
            //    .find('.scroll-indicator.right')
            //    .data('dir', 1);
            this.rightControl = $(this.container.data('control-right')).data('dir', 1);
            this.leftControl = $(this.container.data('control-left')).data('dir', -1);

            this.Init();
            this.events.click.call(this);
            this.events.scroll.call(this);
            return $(this);
        };
        ScrollableSlider.prototype.Init = function() {
            this.SetNiceScroll();
        };
        ScrollableSlider.prototype.events = {
            click: function () {
                var self = this;
                self.rightControl.on('click', function () {
                    var scrollVal = self.scroll.getScrollLeft()
                        + parseInt($(this).data('dir')) * 220;
                    self.scroll.doScrollLeft(scrollVal);
                });
                self.leftControl.on('click', function () {
                    var scrollVal = self.scroll.getScrollLeft()
                        + parseInt($(this).data('dir')) * 220;
                    self.scroll.doScrollLeft(scrollVal);
                });
            },
            scroll: function () {
                var self = this;
            },
        };
        ScrollableSlider.prototype.SetNiceScroll = function() {
            this.scroll = SetNiceScroll(this.container);
        };

        // SetNiceScroll function
        function SetNiceScroll (element) {
            return element.niceScroll({
                cursoropacitymin: 1,
                cursorcolor: 'rgba(255,255,255,.9)',
                cursorborder: 'rgba(0,0,0,.9)',
                background: 'rgba(128,128,128,.2)',
            });
        };

        initscrollableslider = function(){
            var gallery_slider = new ScrollableSlider({
                container: $('.scrollable-slider')
            });
        };
        initscrollableslider();

        // ----------------------------------------------------
    });
})(jQuery);

