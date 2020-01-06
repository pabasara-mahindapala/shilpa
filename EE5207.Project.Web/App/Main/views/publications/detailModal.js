(function () {
    angular.module('app').controller('app.views.publications.detailModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.publication', 'id',
        function ($scope, $uibModalInstance, publicationService, id) {
            var vm = this;

            vm.publication = {};

            var init = function () {
                publicationService.get({ id: id })
                    .then(function (result) {
                        vm.publication = result.data;
                    });
            };

            vm.download = function (publication) {
                publication.downloads++;

                publicationService.update(publication)
                    .success(function () {
                        abp.notify.info("Downloading " + publication.name);
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

            init();
        }
    ]);
})();