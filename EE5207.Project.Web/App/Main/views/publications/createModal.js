(function () {
    angular.module('app').controller('app.views.publications.createModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.publication', 'Upload',
        function ($scope, $uibModalInstance, publicationService, Upload) {
            var vm = this;

            vm.file = {};



            vm.publication = {
                downloads: 0
            };

            vm.save = function () {
                vm.f = document.getElementById('avatar').files[0];
                if (vm.f != null) {
                    vm.publication.filePath = "app/Main/views/files/" + vm.f.name;
                }
                else {
                    vm.publication.filePath = "N/A";
                }

                if (vm.f) {
                    vm.upload(vm.f);
                }

                publicationService
                    .create(vm.publication)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.onFileSelect = function ($files) {
                console.log($files);
            };


            $scope.uploadFile = function (input) {
                //console.log("Changed");

                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#blah')
                            .attr('src', e.target.result)
                            .width(150)
                            .height(200);
                    };

                    reader.readAsDataURL(input.files[0]);
                }

            };

            $scope.onFileSelect = function ($files) {
                Upload.upload({
                    url: '~/App/Main/views/files/',
                    file: $files,
                }).progress(function (e) {
                }).then(function (data, status, headers, config) {
                    // file is uploaded successfully
                    console.log(data);
                });
            };

            vm.upload = function (file) {
                Upload.upload({
                    url: '/api/upload/',
                    data: { file: file, key: 1 }
                }).then(function (resp) {
                    console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
                }, function (resp) {
                    console.log('Error status: ' + resp.status);
                }, function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };


        }
    ]);
})();