#!groovy
properties([disableConcurrentBuilds()])

pipeline {
  agent{
    label 'slave02'
  }
 triggers {pollSCM('* * * * *')} 
 options {
    timestamps()
  }
  stages {
    stage('Create file') {
      steps {
        sh '''cd '/home/slava'
              touch XOR.txt 
              echo "XOR" > "/home/slava/XOR.txt"
           '''
      }
    }
     stage('Send file') {
      steps {
        sh '''scp /home/slava/file.txt 192.168.0.107:/home/slava
              ssh '192.168.0.107'
              cd /home/slava
              cat file.txt
           '''
      }
    }
  }
}
