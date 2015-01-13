/**
 * Jquery Plugin: $myName 1.00
 *
 * $author       Zone dev
 * $email        frontend@thisiszone.com
 * $url          http://www.thisiszone.com/
 * $copyright    Copyright (c) 2013, thisiszone.com. All rights reserved.
 * $version      1.0
 *
 * $notes        Notes
 */


define(['jquery', 'debug'], function($, debug){
    'use strict';

    (function ($) {

        $.fn.myJQueryPlugin = function (options) {

            //Settings
            var settings = $.extend({
                optionOne       : null
            }, options);

            return this.each(function () {

                var $wrapper = $(this);


                function firstFunction () {
                    //my first function
                    $wrapper.text(settings.optionOne);
                    debug.log('I am the output of first jQuery plugin function');
                }

                //Init functions
                firstFunction();

            });
            //end each
        };
        //end jquery extend

    })(jQuery);


}); //end define