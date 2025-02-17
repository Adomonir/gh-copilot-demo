############################################
## DATABASE                               ##
############################################

resource "null_resource" "db_schema" {
  depends_on = [
    azurerm_mssql_database.mssql_database
  ]
  provisioner "local-exec" {
    command = "sqlcmd -U ${local.mssql_server_administrator_login} -P ${local.mssql_server_administrator_login_password} -S ${azurerm_mssql_server.mssql_server.fully_qualified_domain_name} -d ${local.mssql_database_name} -i ../../support/datainit/MYDrivingDB.sql -e"
  }
}

resource "null_resource" "db_datainit" {
  depends_on = [
    null_resource.db_schema
  ]
  provisioner "local-exec" {
    command = "cd ../../support/datainit; bash ./sql_data_init.sh -s ${azurerm_mssql_server.mssql_server.fully_qualified_domain_name} -u ${local.mssql_server_administrator_login} -p ${local.mssql_server_administrator_login_password} -d ${local.mssql_database_name}; cd ../../iac/terraform"
  }
}

############################################
## DOCKER                                 ##
############################################

resource "null_resource" "docker_simulator" {
  depends_on = [
    azurerm_container_registry.container_registry
  ]
  provisioner "local-exec" {
    command = "az acr build --image devopsoh/simulator:latest --registry ${azurerm_container_registry.container_registry.login_server} --file ../../support/simulator/Dockerfile ../../support/simulator"
  }
}

resource "null_resource" "docker_tripviewer" {
  provisioner "local-exec" {
    command = "az acr build --image devopsoh/tripviewer:latest --registry ${azurerm_container_registry.container_registry.login_server} --file ../../support/tripviewer/Dockerfile ../../support/tripviewer"
  }
}

resource "null_resource" "docker_api-poi" {
  provisioner "local-exec" {
    command = "az acr build --image devopsoh/api-poi:${local.apipoi_base_image_tag} --registry ${azurerm_container_registry.container_registry.login_server} --build-arg build_version=${local.apipoi_base_image_tag} --file ../../apis/poi/web/Dockerfile ../../apis/poi/web"
  }
}

resource "null_resource" "docker_api-trips" {
  provisioner "local-exec" {
    command = "az acr build --image devopsoh/api-trips:${local.apitrips_base_image_tag} --registry ${azurerm_container_registry.container_registry.login_server} --build-arg build_version=${local.apitrips_base_image_tag} --file ../../apis/trips/Dockerfile ../../apis/trips"
  }
}

resource "null_resource" "docker_api-user-java" {
  provisioner "local-exec" {
    command = "az acr build --image devopsoh/api-user-java:${local.apiuserjava_base_image_tag} --registry ${azurerm_container_registry.container_registry.login_server} --build-arg build_version=${local.apiuserjava_base_image_tag} --file ../../apis/user-java/Dockerfile ../../apis/user-java"
  }
}

resource "null_resource" "docker_api-userprofile" {
  provisioner "local-exec" {
    command = "az acr build --image devopsoh/api-userprofile:${local.apiuserprofile_base_image_tag} --registry ${azurerm_container_registry.container_registry.login_server} --build-arg build_version=${local.apiuserprofile_base_image_tag} --file ../../apis/userprofile/Dockerfile ../../apis/userprofile"
  }
}
############################################
## CONTAINER REGISTRY                     ##
############################################

resource "azurerm_container_registry" "container_registry" {
  name                     = "mycontainerregistry"
  resource_group_name      = azurerm_resource_group.resource_group.name
  location                 = azurerm_resource_group.resource_group.location
  sku                      = "Basic"
  admin_enabled            = true
  georeplication_locations = ["eastus2"]
}

############################################
## AZURE COGNITIVE SERVICES               ##
############################################

resource "azurerm_cognitive_account" "cognitive_account" {
  name                = "mycognitiveaccount"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_resource_group.resource_group.location
  kind                = "CognitiveServices"
  sku_name            = "S0"
}

resource "azurerm_cognitive_customvision_account" "customvision_account" {
  name                = "mycustomvisionaccount"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_resource_group.resource_group.location
  kind                = "CustomVision.Training"
  sku_name            = "S0"
}

