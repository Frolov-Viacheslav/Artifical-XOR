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
        sh '''cd '/home/slava'
              echo 'Kraken' > file.txt 
           '''
      }
    }
     stage('Send file') {
      steps {
        sh '''scp file.txt 192.168.0.107:/home/slava
              ssh '192.168.0.107'
              cd /home/slava
              cat file.txt
           '''
      }
    }
  }
}
