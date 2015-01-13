/**
	* Example plugin
	*
	* $author       Zone dev
	* $email        frontend@thisiszone.com
	* $url          http://www.thisiszone.com/
	* $copyright    Copyright (c) 2012, thisiszone.com. All rights reserved.
	* $version      1.0
	*
	* $notes        Notes
*/

define(['debug'], function(debug){
    'use strict';

    var MyPlugin = function MyPlugin(selector){
	    //Constructor
	    this._el = document.querySelector(selector);
	};

	//Add a new property and method to our plugin
	MyPlugin.prototype.anotherProperty = 'I am a property of the example JS plugin';
	MyPlugin.prototype.aMethod = function aMethod(testArgument) {
		var _self = this;
		_self.testArgument = true;
		return _self;
	};
	MyPlugin.prototype.anotherMethod = function anotherMethod() {
		debug.log('I am a DOM element used by a JS plugin: ', this._el);
	};

	return {MyPlugin:MyPlugin, otherProperties:MyPlugin.prototype};

}); //end define