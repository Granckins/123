﻿var appCtrl = function ($scope, $http) {
     
    $scope.panels = [
    {
        "category": "Artifice",
        "date": "May 2016",
        "icon": "cog",
    },
    {
        "category": "Lore",
        "date": "April 2016",
        "icon": "database",
    },
    {
        "category": "Planeswalking",
        "date": "February 2016",
        "icon": "leaf",
    }
    ];

}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
appCtrl.$inject = ['$scope', '$http'];