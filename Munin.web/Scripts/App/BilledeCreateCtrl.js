muninApp.controller('billedeCreateCtrl', function ($scope, $timeout) {

    $scope.model = {};
    $scope.model.billedId = 'JensenTonajt';
    $scope.model.checked = true;

    $scope.va1 = '123';

    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.opened = false;

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.toggleMin = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1,
        showWeeks: false        
};

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'dd-MM-yyyy', 'shortDate'];
    $scope.format = $scope.formats[3];

});