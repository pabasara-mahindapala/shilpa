(function () {
    angular.module('app').controller('app.views.publications.index', [
        '$scope', '$timeout', '$uibModal', 'abp.services.app.publication',
        function ($scope, $timeout, $uibModal, publicationService) {
            var vm = this;

            vm.publications = [];

            function getPublications() {
                publicationService.getAll({}).then(function (result) {
                    vm.publications = result.data.items;
                });
            }

            vm.openPublicationCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/publications/createModal.cshtml',
                    controller: 'app.views.publications.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getPublications();
                });
            };

            vm.openPublicationDetailModal = function (publication) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/publications/detailModal.cshtml',
                    controller: 'app.views.publications.detailModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return publication.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getPublications();
                });
            };

            vm.delete = function (publication) {
                abp.message.confirm(
                    "Delete publication '" + publication.name + "'?",
                    function (result) {
                        if (result) {
                            publicationService.delete({ id: publication.id })
                                .then(function () {
                                    abp.notify.info("Deleted publication: " + publication.name);
                                    getPublications();
                                });
                        }
                    });
            };

            vm.download = function (publication) {
                publication.downloads++;

                publicationService.update(publication)
                    .success(function () {
                        getPublications();
                    });
            };

            vm.refresh = function () {
                getPublications();
            };

            getPublications();
        }
    ]);
})();