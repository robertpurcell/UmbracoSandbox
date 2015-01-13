/**
    * Main app & config for require.js
    *
    * $author       Zone dev
    * $email        frontend@thisiszone.com
    * $url          http://www.thisiszone.com/
    * $copyright    Copyright (c) 2013, thisiszone.com. All rights reserved.
    * $version      1.0
    *
    * $notes        Notes
*/


/* -------------------------------------------------------------------------- */
/* ---------- Configure relative paths -------------------------------------- */
/* -------------------------------------------------------------------------- */

require.config({
    paths: {
        //If IE8 needs to be supported switch to jquery 1.11.0
        jquery: 'libs/jquery',
        debug: 'modules/debug',
        exampleModule: 'modules/example-module',
        examplePlugin: 'plugins/example-plugin',
        exampleJqueryPlugin: 'plugins/example-jquery-plugin'
    }

});

/* -------------------------------------------------------------------------- */
/* ---------- Initialize app ------------------------------------------------ */
/* -------------------------------------------------------------------------- */

require(['jquery', 'debug', 'base', 'exampleModule', 'examplePlugin', 'exampleJqueryPlugin'], function($, debug, base, module, MyPlugin) {
    'use strict';

    /* ---------- Global modules -------------------------------------------- */
    base.init();

    /* ---------- App modules ----------------------------------------------- */
    module.init();

    /* ---------- Plugins --------------------------------------------------- */
    //Create new instance of JS vanilla plugin
    var exampleInstance = new MyPlugin.MyPlugin('.mySelector');
    //use new methods and properties
    exampleInstance.aMethod('something');
    exampleInstance.anotherMethod();
    debug.log(exampleInstance.anotherProperty);

    //initialise jQuery plugin
    $('.myJQuerySelector').myJQueryPlugin({optionOne: 'I am the output of a typical jQuery plugin'});

    /* ---------- Utilities ------------------------------------------------- */
    if (!base.ltIe9) {
        //load something that is for IE9 or greater
        debug.log('IE9 or other browser');
    } else {
        debug.log('IE9 or less');
    }
});