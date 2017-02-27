muninApp.controller('billedeCreateCtrl', function ($scope, $timeout, $http) {

    //https://angular-ui.github.io/ui-select/

    var vm = this;

    vm.materialer = [];
    vm.journaler = [];
    vm.model = {};
    vm.model.billedId = 0;
    vm.model.billedIndex = '';
    vm.model.numOrdning = '';
    vm.model.ordning = '';
    vm.model.cdNr = '';
    vm.model.fotograf = '';
    vm.model.format = ''
    vm.model.klausul = false;
    vm.model.ophavsret = true;
    vm.model.note = '';
    vm.model.datering = new Date();


    vm.model.checked = true;
    vm.model.selectedId = 0;

    $scope.dataset = [];
    $scope.selectedItem = {};

    $scope.initpage = function () {
        var id = angular.element('#id').val();
        $http.get('/billeders/load/' + id).then(function (result) {
                vm.materialer = result.data.materialeList;
                vm.journaler = result.data.journalList;
            },
        function(result) {
            console.log(result);
        });
    }

    vm.person = {};

    vm.person.selectedValue = { name: 'Adam', email: 'adam@email.com', age: 12, country: 'United States' };
    vm.person.selectedSingle = 'Samantha';
    vm.person.selectedSingleKey = '5';
    // To run the demos with a preselected person object, uncomment the line below.
    //vm.person.selected = vm.person.selectedValue;

    vm.people = [
      { name: 'Adam', email: 'adam@email.com', age: 12, country: 'United States' },
      { name: 'Amalie', email: 'amalie@email.com', age: 12, country: 'Argentina' },
      { name: 'Estefanía', email: 'estefania@email.com', age: 21, country: 'Argentina' },
      { name: 'Adrian', email: 'adrian@email.com', age: 21, country: 'Ecuador' },
      { name: 'Wladimir', email: 'wladimir@email.com', age: 30, country: 'Ecuador' },
      { name: 'Samantha', email: 'samantha@email.com', age: 30, country: 'United States' },
      { name: 'Nicole', email: 'nicole@email.com', age: 43, country: 'Colombia' },
      { name: 'Natasha', email: 'natasha@email.com', age: 54, country: 'Ecuador' },
      { name: 'Michael', email: 'michael@email.com', age: 15, country: 'Colombia' },
      { name: 'Nicolás', email: 'nicolas@email.com', age: 43, country: 'Colombia' }
    ];


    $scope.disabled = false;
    

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

    $scope.selectMe = function() {
        alert($scope.model.selectedId);
    }

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'dd-MM-yyyy', 'shortDate'];
    $scope.format = $scope.formats[3];

});