﻿var phonecatApp = angular.module('phonecatApp', []);

//phonecatApp.controller('PhoneListController', function PhoneListController($scope) {
//	$scope.phones = [
//		{
//			name: 'Nexus S',
//			snippet: 'Fast just got faster with Nexus S.'
//		},
//		{
//			name: 'Motorola XOOM™ with Wi-Fi',
//			snippet: 'The Next, Next Generation tablet.'
//		},
//		{
//			name: 'MOTOROLA XOOM™',
//			snippet: 'The Next, Next Generation tablet.'
//		}
//	];
//});

angular.module('phonecatApp').
	component('greetUser', {
		template: 'Hello, {{$ctrl.user}}!',
		controller: function GreetUserController() {
			this.user = 'world';
		}
	});

angular.module('phonecatApp').
	component('phoneList', {
		template:'<ul>' + 
			'<li ng-repeat="phone in $ctrl.phones">' +
			'<span>{{phone.name}}</span>' + 
			'<p>{{phone.snippet}}</p>' +
			'</li>' + 
			'</ul>',
		controller: function PhoneListController() {
			this.phones = [
			{
        		name: 'Nexus S',
        		snippet: 'Fast just got faster with Nexus S.'
			}, {
        		name: 'Motorola XOOM™ with Wi-Fi',
        		snippet: 'The Next, Next Generation tablet.'
			}, {
        		name: 'MOTOROLA XOOM™',
        		snippet: 'The Next, Next Generation tablet.'
			}
			];
		}
	});