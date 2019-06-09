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
    stage('First step') {
      steps {
        echo 'Kraken'
      }
    }
  }
}
