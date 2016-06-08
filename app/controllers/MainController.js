NotDolls.controller('MainController', [
  '$http',
  '$scope',

  function ($http, $scope) {

    $scope.figurines = [];

    $http.get('http://10.0.0.103:5000/api/Inventory')
    .success(inv => $scope.figurines = inv)
  }

]);