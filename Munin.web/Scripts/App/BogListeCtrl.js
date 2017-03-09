muninApp.controller('BoglisteCtrl', function ($scope, $http, $location) {
    var saveSettings = function () {
        var settings = JSON.stringify($scope.query);
        localStorage.setItem($location.path(), settings);
    }

    var loadSettings = function () {
        var settings = localStorage.getItem($location.path());
        if (settings !== null) {
            return JSON.parse(settings);
        }
        return null;
    }

    var updatePanel = function () {
        $scope.pages = [];
        if ($scope.pageResult.pages > 0) {
            var start = 0;
            if (($scope.query.p > Math.floor($scope.gap / 2)) && ($scope.gap < $scope.pageResult.pages)) {
                start = $scope.query.p - Math.floor($scope.gap / 2);
            }
            var end = start + $scope.gap + 1;
            if (end > $scope.pageResult.pages) {
                end = $scope.pageResult.pages;
            }
            for (var i = start; i < end + 1; i++) {
                $scope.pages.push(i);
            }
            console.log($scope.pages);
        }
    }

    $scope.pages = [];


    $scope.openDatePicker = function ($event) {
        $scope.datePickerOpen = true;
    };

    $scope.sort = {
        sortingOrder: 'id',
        reverse: false
    };

    $scope.pageLoading = true;

    $scope.gap = 5; //antal side-felter der skal vises i panel
    $scope.pageSizes = [5, 10, 25, 50];

    $scope.query = loadSettings();
    if ($scope.query === null) {
        $scope.query = {};
        $scope.query.q = '';
        $scope.query.size = 10;
        $scope.query.p = 1;
        $scope.query.o = 'asc';
        $scope.query.s = 'id';
    }
    $scope.items = [];

    $scope.loadPage = function () {
        var query = $scope.query;
        $http.post('/Biblioteks/BogListe/', query).then(function (result) {
            console.log(result);
            $scope.pageResult = result.data;
            $scope.items = $scope.pageResult.data;
            $scope.pageLoading = false;
            updatePanel();
        });
    };

    $scope.search = function () {
        $scope.query.p = 0;
        saveSettings();
        $scope.loadPage();
    };

    $scope.prevPage = function () {
        if ($scope.query.p > 0) {
            $scope.query.p--;
            saveSettings();
            $scope.loadPage();
        }
    };

    $scope.nextPage = function () {
        if ($scope.query.p < $scope.pageResult.pages) {
            $scope.query.p++;
        }
        saveSettings();
        $scope.loadPage();
    };

    $scope.perPage = function () {
        $scope.query.p = 0;
        saveSettings();
        $scope.loadPage();
    };

    $scope.setPage = function () {
        $scope.query.p = this.n;
        saveSettings();
        $scope.loadPage();
        //$scope.currentPage = this.n;
    };


    $scope.sort_by = function (newSortingBy) {
        $scope.query.s = newSortingBy;
        $scope.query.o = ($scope.query.o === 'asc') ? 'desc' : 'asc';
        $scope.query.p = 0;
        saveSettings();
        $scope.loadPage();
    }

    $scope.selectedCls = function (column) {
        if ($scope.query.s === column) {
            return ('fa fa-sort-' + (($scope.query.o.toLowerCase() === 'desc') ? 'desc' : 'asc'));
        }
        return 'fa fa-sort';
    };
    // functions have been describe process the data for display
});