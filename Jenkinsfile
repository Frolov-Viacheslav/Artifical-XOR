#!groovy
properties([disableConcurrentBuilds()])

pipeline {
  agent{
    label 'slave02'
  }
 //triggers {pollSCM('* * * * *')} 
 options {
    timestamps()
  }
  stages {
    stage('Create file') {
      steps {
        cd /home/slava
        echo 'Kraken' > file.txt
        //scp file.txt 192.168.0.107:/home/slava
        ssh '192.168.0.107'
        cd /home/slava
        touch file.txt
      }
    }
  }
}
