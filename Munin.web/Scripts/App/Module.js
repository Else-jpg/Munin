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
        template: '<div class="form-group"><div class="row"><label class="control-label col-md-2">{{label}}</label><div class="col-md-10"><i class="btn fa fa-check-square-o fa-2x" aria-hidden="true"></i></div></div></div>',
        scope: {
            label: '@',
            model: '='
        },
        transclustion: true
    }
})


