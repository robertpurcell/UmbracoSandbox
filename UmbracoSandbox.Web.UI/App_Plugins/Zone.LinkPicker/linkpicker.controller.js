﻿'use strict'

angular.module("umbraco").controller("Zone.LinkPickerController", function ($scope, dialogService, iconHelper, entityResource) {
    var documentIds = []
      , mediaIds = []

    $scope.renderModel = []
    $scope.sortableOptions = { handle: '.handle' }

    if ($scope.model.value) {
        _.each($scope.model.value, function (item, i) {
            $scope.renderModel.push(new Link(item))
            if (item.id) {
                (item.isMedia ? mediaIds : documentIds).push(item.id)
            }
        })
    }

    var setIcon = function (nodes) {
        if (_.isArray(nodes)) {
            _.each(nodes, setIcon)
        } else {
            var item = _.find($scope.renderModel, function (item) {
                return +item.id === nodes.id
            })
            item.icon = iconHelper.convertFromLegacyIcon(nodes.icon);
        }
    }

    entityResource.getByIds(documentIds, 'Document').then(setIcon)
    entityResource.getByIds(mediaIds, 'Media').then(setIcon)

    $scope.openLinkPicker = function () {
        dialogService.linkPicker({ callback: $scope.onContentSelected })
    }

    $scope.edit = function (index) {
        var link = $scope.renderModel[index]
        dialogService.linkPicker({
            currentTarget: {
                id: link.isMedia ? null : link.id // the linkPicker breaks if it get an id for media
              , index: index
              , name: link.name
              , url: link.url
              , target: link.target
              , isMedia: link.isMedia
            }
          , callback: $scope.onContentSelected
        })
    }

    $scope.remove = function (index) {
        $scope.renderModel.splice(index, 1)
        $scope.model.value = $scope.renderModel
    }

    $scope.$watch(
        function () {
            return $scope.renderModel.length
        }
      , function (newVal) {
          if ($scope.renderModel.length) {
              $scope.model.value = $scope.renderModel
          } else {
              $scope.model.value = null
          }

          if (+1 < $scope.renderModel.length) {
              $scope.multiUrlPickerForm.maxCount.$setValidity('maxCount', false)
          } else {
              $scope.multiUrlPickerForm.maxCount.$setValidity('maxCount', true)
          }
      }
    )

    $scope.$on("formSubmitting", function (ev, args) {
        if ($scope.renderModel.length) {
            $scope.model.value = $scope.renderModel
        } else {
            $scope.model.value = null
        }
    })


    $scope.onContentSelected = function (e) {
        var link = new Link(e);

        if (e.index != null) {
            $scope.renderModel[e.index] = link
        } else {
            $scope.renderModel.push(link)
        }

        if (e.id && e.id > 0) {
            entityResource.getById(e.id, e.isMedia ? 'Media' : 'Document').then(setIcon)
        }

        $scope.model.value = $scope.renderModel
    }

    function Link(link) {
        this.id = link.id;
        this.name = link.name || link.url;
        this.url = link.url;
        this.target = link.target;
        this.isMedia = link.isMedia;
        this.icon = link.icon || 'icon-link';
    }
})