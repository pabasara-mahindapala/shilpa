(function () {
    angular.module('app').controller('app.views.courses.detailModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.course', 'id',
        function ($scope, $uibModalInstance, courseService, id) {
            var vm = this;

            vm.course = {};
            vm.students = [];


            var init = function () {
                courseService.get({ id: id })
                    .then(function (result) {
                        vm.course = result.data;
                        courseService.getStudents(id)
                            .then(function (res) {
                                vm.students = res.data;
                                courseService.markAttendance(vm.course)
                                    .then(function () {
                                        alert("Started course : " + vm.course.name);
                                    });
                            });
                    });
            };


            vm.markStudent = function (studentId) {
                courseService.markStudent(studentId, vm.course.id)
                    .then(function (resu) {
                        courseService.getAttendance(resu.data)
                            .then(function (res) {
                                vm.attendance = res.data;

                                vm.attendance.presentDays++;
                                courseService.get({ id: id })
                                    .then(function (re) {
                                        vm.attendance.percentage = (vm.attendance.presentDays / re.data.conductedDays) * 100;
                                        courseService.updateAttendance(vm.attendance)
                                            .then(function (r) {
                                                console.log(r);
                                                abp.notify.success("Done!");
                                            });
                                    });
                            });
                    });

            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };


            init();
        }
    ]);
})();