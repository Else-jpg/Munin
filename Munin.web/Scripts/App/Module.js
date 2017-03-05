var muninApp = angular.module('muninApp', ['ngSanitize', 'ui.bootstrap', 'ui.bootstrap.datetimepicker', 'angular.filter', 'ui.select'])

.directive("aspNetTextElement", function () {
        return {
            template: '<div class="form-group"><div class="row"><label class="control-label col-md-2">{{label}}</label><div class="col-md-10"><input type="text" class="form-control" ng-model="model" placeholder="{{placeholder}}"/></span></div></div></div>',
            scope: {
                label: '@',
                model: '=',
                placeholder:'@'

            },
            transclustion: true
        }
})
.directive("aspNetCheckElement", function () {
    return {
        template: '<div class="form-group"><div class="row"><label class="control-label col-md-2">{{label}}</label><div class="col-md-10"><i class=" btn fa fa-check-square-o fa-2x" ng-click="click()" aria-hidden="true" ng-class="model? \' fa-check-square-o fa-2x \' : \'fa-square-o fa-2x \' "></i></div></div></div>',
        scope: {
            label: '@',
            model: '='
        },
        transclustion: true,
        controller: function ($scope, $element) {
            $scope.clicked = 0;
            $scope.click = function () {
                $scope.model = !$scope.model;
            }
        }

    }
})
.directive("aspNetTextArea", function() {
        return {
            template: '<div class="form-group"><div class="row"><label class="control-label col-md-2">{{label}}</label><div class="col-md-10"><textarea class="form-control" ng-model="model" style="height:120px"/></span></div></div></div>',
            scope: {
                label: '@',
                model: '='
            },
            transclustion: true
        }
})
.directive("aspNetError",function() {
    return {
        template: '<div class="form-group"><div class="row">'
                + '<div class="control-label col-md-2">'
                + '</div>'
                + '<div class="col-md-10">'                
                + '<div class="panel panel-danger" ng-show="showMessage()">'
                + '<div class="panel-heading">'
                + '<div class="panel-title"><i class="glyphicon glyphicon-warning-sign"></i>&nbsp;{{message}}</div>'
                + '</div>'
                + '</div>'
                + '</div>'
                + '</div>',
        scope: {
         message:'@'   
        },
        controller: function ($scope) {
            $scope.showMessage = function() {
                if ($scope.message == null || $scope.message === '')
                    return false;
                return true;
            }
        }
    }        
})
.directive("aspNetSelect", function() {
    return {            
            template: '<div class="form-group">'
                + '<div class="row">'
                + '<label class="control-label col-md-2">{{label}}</label>'
                + '<div class="col-md-10">'
                + '<ui-select ng-model="model" theme="bootstrap" on-select="select()">'
                + '<ui-select-match placeholder="Vælg en undersag">{{$select.selected.text}}</ui-select-match>'
                + '	<ui-select-choices repeat="item in source | filter: $select.search">'
                + '<span ng-bind-html="item.text"></span>'
                + '</ui-select-choices>'
                + '</ui-select>'
                + '</div></div>',
            scope: {
                label: '@',
                source: '=data',
                plcholder: '@',
                model: '='
            },
            controller: function($scope, $filter) {
                $scope.clicked = 0;
                $scope.select = function () {
                    console.log($scope.model);
                }
            },
            transclustion:true
        }
    })