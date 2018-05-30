var ImportWordController = function ($scope, $q, $timeout,FileUploader) {
 
    var vm = this;
    vm.texttext = "12345";
    vm.selectedStep = 0;
    vm.logimport = [];
    vm.stepProgress =1;
    vm.maxStep = 3;
    vm.showBusyText = false;
    vm.stepData = [
        { step: 1, completed: false, optional: false, data: {} },
        { step: 2, completed: false, optional: false, data: {} },
        { step: 3, completed: false, optional: false, data: {} },
    ];
    $scope.upload = function () {
        angular.element(document.querySelector('#fileInput')).click();
    };
    vm.enableNextStep = function nextStep() {
        //do not exceed into max step
        if (vm.selectedStep >= vm.maxStep) {
            return;
        }
        //do not increment vm.stepProgress when submitting from previously completed step
        if (vm.selectedStep === vm.stepProgress - 1) {
            vm.stepProgress = vm.stepProgress + 1;
        }
        vm.selectedStep = vm.selectedStep + 1;
    }

    vm.moveToPreviousStep = function moveToPreviousStep() {
        if (vm.selectedStep > 0) {
            vm.selectedStep = vm.selectedStep - 1;
        }
    }

   
    vm.submitCurrentStep = function submitCurrentStep(stepData, isSkip) {
        var deferred = $q.defer();
        vm.showBusyText = true;
        console.log('On before submit');
        if (!stepData.completed && !isSkip) {
            //simulate $http
            $timeout(function () {
                vm.showBusyText = false;
                console.log('On submit success');
                deferred.resolve({ status: 200, statusText: 'success', data: {} });
                //move to next step when success
                stepData.completed = true;
                vm.enableNextStep();
            }, 1000)
        } else {
            vm.showBusyText = false;
            vm.enableNextStep();
        }
    }
    vm.NextStep = function NextStep() {
          
          
            
                vm.showBusyText = false;
              
               
                vm.enableNextStep();
           
        
    }
    vm.restart = function restart() {
        vm.selectedStep = 0;
        vm.stepProgress = 1;
        uploaderword.clearQueue(); 
        vm.logimport = [];
    }
    vm.preview = function preview() {
       
        vm.showBusyText = false; 
        vm.enableNextStep();

    }
    var uploaderword = $scope.uploader = new FileUploader({
        type: "POST",
        url: 'Upload/UploadWord',
        headers: [{ name: 'Accept', value: 'application/json' }],
        
    });
    var uploaderpreview = $scope.uploaderpreview = new FileUploader({
        type: "POST",
        url: 'Upload/UploadWordPreview',
        headers: [{ name: 'Accept', value: 'application/json' }],

    });
 
    // FILTERS

    // a sync filter
    uploaderword.filters.push({
        name: 'syncFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            console.log('syncFilter');
            
            return this.queue.length < 10;
        }
    });

    // an async filter
    uploaderword.filters.push({
        name: 'asyncFilter',
        fn: function (item /*{File|FileLikeObject}*/, options, deferred) {
            console.log('asyncFilter');
            setTimeout(deferred.resolve, 1e3);
        }
    });

    // CALLBACKS

    uploaderword.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
        console.info('onWhenAddingFileFailed', item, filter, options);
    };
    uploaderword.onAfterAddingFile = function (fileItem) {
      
    };
    uploaderword.onAfterAddingAll = function (addedFileItems) { 
        console.info('onAfterAddingAll', addedFileItems);
    };
    uploaderword.onBeforeUploadItem = function (item) {
        console.info('onBeforeUploadItem', item);
    };
    uploaderword.onProgressItem = function (fileItem, progress) {
        console.info('onProgressItem', fileItem, progress);
    };
    uploaderword.onProgressAll = function (progress) {

        console.info('onProgressAll', progress);
    };
    uploaderword.onSuccessItem = function (fileItem, response, status, headers) {
        vm.logimport = vm.logimport.concat(response);
        console.info('onSuccessItem', fileItem, response, status, headers);
    };
   vm.numberToDisplay=5;
    vm.loadMore = function() {
        if (vm.logimport + 5 < vm.logimport.length) {
            vm.logimport += 5;
        } else {
            vm.logimport = vm.logimport.length;
        }
    };
    vm.HasErrorResp = function () { 
        var counter = 0;
        for (var i = 0; i < vm.logimport.length; i++) {
            if (vm.logimport[i].result == false) {
                counter++;
            }
        }
        if (counter == 0)
            return 1;
        else {
            if (counter == vm.logimport.length)
                return 3;
            else
                return 2;
        }
        };
    uploaderword.onErrorItem = function (fileItem, response, status, headers) {
        console.info('onErrorItem', fileItem, response, status, headers);
    };
    uploaderword.onCancelItem = function (fileItem, response, status, headers) {
        console.info('onCancelItem', fileItem, response, status, headers);
    };
    uploaderword.onCompleteItem = function (fileItem, response, status, headers) {
        console.info('onCompleteItem', fileItem, response, status, headers);
    };
    uploaderword.onCompleteAll = function () { 
        vm.selectedStep = 3;
        vm.stepProgress = 3;
        console.info('onCompleteAll');
    };

    console.info('uploader', uploader);
}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
ImportWordController.$inject = ['$scope', '$q', '$timeout','FileUploader'];