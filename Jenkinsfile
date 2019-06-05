#!groovy
properties([disableConcurrentBuilds()])

pipeline {
  agent{
    label 'master'
  }
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
