#!/usr/bin/env python

from setuptools import setup, Command, find_packages


with open('requirements.txt') as f:
    required = f.read().splitlines()

setup(name='unityagents',
<<<<<<< HEAD
      version='0.3.0',
=======
      version='0.4.0',
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
      description='Unity Machine Learning Agents',
      license='Apache License 2.0',
      author='Unity Technologies',
      author_email='ML-Agents@unity3d.com',
      url='https://github.com/Unity-Technologies/ml-agents',
      packages=find_packages(),
      install_requires = required,
      long_description= ("Unity Machine Learning Agents allows researchers and developers "
       "to transform games and simulations created using the Unity Editor into environments "
       "where intelligent agents can be trained using reinforcement learning, evolutionary " 
       "strategies, or other machine learning methods through a simple to use Python API.")
     )
