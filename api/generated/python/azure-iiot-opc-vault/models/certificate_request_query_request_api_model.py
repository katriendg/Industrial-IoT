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


class CertificateRequestQueryRequestApiModel(Model):
    """Certificate request query model.

    :param entity_id: The entity id to filter with
    :type entity_id: str
    :param state: The certificate request state. Possible values include:
     'New', 'Approved', 'Rejected', 'Failure', 'Completed', 'Accepted'
    :type state: str or ~azure-iiot-opc-vault.models.CertificateRequestState
    """

    _attribute_map = {
        'entity_id': {'key': 'entityId', 'type': 'str'},
        'state': {'key': 'state', 'type': 'CertificateRequestState'},
    }

    def __init__(self, entity_id=None, state=None):
        super(CertificateRequestQueryRequestApiModel, self).__init__()
        self.entity_id = entity_id
        self.state = state
