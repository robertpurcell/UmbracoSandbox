/**
 * Example module module
 *
 * $author       Zone dev
 * $email        frontend@thisiszone.com
 * $url          http://www.thisiszone.com/
 * $copyright    Copyright (c) 2013, thisiszone.com. All rights reserved.
 * $version      1.0
 *
 * $notes        Notes
 */

var requireLocalized = requireLocalized || {};

define(['debug'], function(debug) {
	'use strict';

	//private variable
	var _privateVar = 'I am private var';

	//private method
	var _privateMethod = function () {
		debug.log('I am a private function and I use a ' + module.privateVar);
	};

	var module = {


		//public members(use this)
		publicVar: 'I am public var',

		//privileged method
		//can access private & public variables
		privilegedMethod: function privilegedMethodFn() {
			debug.log('privileged function said: ' + module.publicVar + ' and ' + module.privateVar);
		},

		init: function initModule() {
			module.privilegedMethod();
		}

	};

	//Call private method
	_privateMethod();

	return module;

});