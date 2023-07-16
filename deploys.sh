#!/bin/bash
appName="tallermecanico-backend-api"
appSourceDir="/api/tallermecanico-backend-api"
startTime=`date +%s`

# Nos movemos al directorio donde se encuentra el repositorio
cd $appSourceDir

# Se setea el branch a subir y ambiente en el cual se quiere deployar(por default se deploya para producciÃƒÆ’Ã‚Â³n, desde master)
branch=${1:-master}

# Valido los enviroments
if [ $environment != "master" ] ; then
	echo "$(tput setaf 1)Invalid enviroment"
	tput sgr0
	exit 1
fi

# Se pullean los cambios del repo para el branch indicado
echo "Updating local repository..."
git stash
git fetch --all
git checkout $branch
git merge $branch origin/$branch
git checkout stash -- appsettings.json
git stash clear

# Si ya hay un container corriendo para esta aplicaciÃƒÂ¯Ã‚Â¿Ã‚Â½n, se lo elimina
if [ "$(docker ps -a | grep $appName)" ]; then
	echo "Removing container..."
	docker stop $appName
	docker rm $appName
fi

version=$(sed -n 's:.*<AssemblyVersion>\(.*\)</AssemblyVersion>.*:\1:p' TallerMecanicoDiWork.csproj)

# Se crea la imagen para el ambiente indicado, y se la tagea con la versiÃƒÂ¯Ã‚Â¿Ã‚Â½n
echo "Building image (v$environment)..."
docker build -t $appName:$version . --build-arg appsettings=$environment 

# Se levanta el container de la aplicaciÃƒÂ¯Ã‚Â¿Ã‚Â½n
echo "Running container..."
docker run --name $appName -d -v /controles:/controles --net=host $appName:$version

# Limpio containers e imÃƒÂ¯Ã‚Â¿Ã‚Â½genes huÃƒÂ¯Ã‚Â¿Ã‚Â½rfan@s
docker container prune -f
docker image prune -f

endTime=`date +%s`

# Imprimo tiempo transcurrido
echo "Total time: $((endTime-startTime))"	
