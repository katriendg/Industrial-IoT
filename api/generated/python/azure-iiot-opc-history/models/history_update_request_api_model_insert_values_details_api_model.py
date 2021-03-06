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


class HistoryUpdateRequestApiModelInsertValuesDetailsApiModel(Model):
    """Request node history update.

    :param node_id: Node to update
    :type node_id: str
    :param browse_path: An optional path from NodeId instance to
     the actual node.
    :type browse_path: list[str]
    :param details: The HistoryUpdateDetailsType extension object
     encoded as json Variant and containing the tunneled
     update request for the Historian server. The value
     is updated at edge using above node address.
    :type details: ~azure-iiot-opc-history.models.InsertValuesDetailsApiModel
    :param header: Optional request header
    :type header: ~azure-iiot-opc-history.models.RequestHeaderApiModel
    """

    _validation = {
        'details': {'required': True},
    }

    _attribute_map = {
        'node_id': {'key': 'nodeId', 'type': 'str'},
        'browse_path': {'key': 'browsePath', 'type': '[str]'},
        'details': {'key': 'details', 'type': 'InsertValuesDetailsApiModel'},
        'header': {'key': 'header', 'type': 'RequestHeaderApiModel'},
    }

    def __init__(self, details, node_id=None, browse_path=None, header=None):
        super(HistoryUpdateRequestApiModelInsertValuesDetailsApiModel, self).__init__()
        self.node_id = node_id
        self.browse_path = browse_path
        self.details = details
        self.header = header
