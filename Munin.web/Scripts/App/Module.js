var muninApp = angular.module('muninApp', ['ui.bootstrap', 'ui.bootstrap.datetimepicker'])

.directive("aspNetTextElement", function () {
        return {
            template: '<div class="form-group"><div class="row"><label class="control-label col-md-2">{{label}}</label><div class="col-md-10"><input type="text" class="form-control" ng-model="model"/></span></div></div></div>',
            scope: {
                label: '@',
                model: '='
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


