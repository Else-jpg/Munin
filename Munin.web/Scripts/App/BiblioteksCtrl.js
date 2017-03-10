muninApp.controller('biblioteksCtrl', function ($scope, $timeout, $http) {

    //https://angular-ui.github.io/ui-select/

    var vm = this;

    vm.journaler = [];
    vm.model = {};
    vm.model.biblioteksId = 0;
    vm.model.Bogkode = '';
    vm.model.Titel = '';
    vm.model.Forfatter = '';
    vm.model.Udgivet = '';
    vm.model.DK5 = '';
    vm.model.Forlag = '';
    vm.model.Note = '';
    vm.model.Indlevering = '';
    vm.model.Journal = '';
    vm.model.JournalID = '';
    vm.model.Ordningsord = '';
    vm.model.Redaktor = '';
    vm.model.Samlemappe = '';
    vm.model.Undertitel = '';

    vm.journal = {};
    vm.materiale = {};

    $scope.dataset = [];
    $scope.selectedItem = {};
    $scope.pageLoaded = false;

    var getMateriale = function(matId) {
        for (i = 0; i < vm.materialer.length; i++) {
            if (vm.materialer[i].value === matId)
                return vm.materialer[i];
        }
        return vm.materialer[0];
    };

    $scope.initpage = function() {
        var id = angular.element('#ID').val();
        $http.get('/biblioteks/load/' + id).then(function(result) {
                console.log(result);
                vm.model = result.data.model;
                vm.journal.value = vm.model.journalID;
                vm.journal.text = vm.model.journal;
                vm.journaler = result.data.journalList;
                $scope.pageLoaded = true;
            },
            function(result) {
                vm.message = "Der opstod en fejl i forbindelse med at vise siden.";
                console.log(result);
            });
    };

    $scope.save = function() {
        if (vm.journal != null) {
            vm.model.journalID = vm.journal.value;
            vm.model.journal = vm.journal.text;
        }
        if (vm.materiale != null) {
            vm.model.materiale = vm.materiale.value;
        }
        $http.post('/biblioteks/save/', vm.model).then(function(result) {
                if (result.data.success === true)
                    window.location.href = "/biblioteks/index";
                vm.message = result.data.message;
            },
            function(result) {
                console.log(result);
                vm.message = 'Der opstod en fejl.';
            });
    };

    $scope.disabled = false;
});