# coding=utf-8
# --------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See License.txt in the project root for
# license information.
#
# Code generated by Microsoft (R) AutoRest Code Generator 2.3.33.0
# Changes may cause incorrect behavior and will be lost if the code is
# regenerated.
# --------------------------------------------------------------------------

from msrest.serialization import Model


class NodePathTargetApiModel(Model):
    """Node path target.

    :param browse_path: The target browse path
    :type browse_path: list[str]
    :param target: Target node
    :type target: ~azure-iiot-opc-twin.models.NodeApiModel
    :param remaining_path_index: Remaining index in path
    :type remaining_path_index: int
    """

    _attribute_map = {
        'browse_path': {'key': 'browsePath', 'type': '[str]'},
        'target': {'key': 'target', 'type': 'NodeApiModel'},
        'remaining_path_index': {'key': 'remainingPathIndex', 'type': 'int'},
    }

    def __init__(self, browse_path=None, target=None, remaining_path_index=None):
        super(NodePathTargetApiModel, self).__init__()
        self.browse_path = browse_path
        self.target = target
        self.remaining_path_index = remaining_path_index
