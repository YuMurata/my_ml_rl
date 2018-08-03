import unittest.mock as mock
import pytest

import numpy as np
import tensorflow as tf

from unitytrainers.ppo.models import PPOModel
<<<<<<< HEAD
from unityagents import UnityEnvironment


def test_ppo_model_continuous():
    c_action_c_state_start = '''{
      "AcademyName": "RealFakeAcademy",
      "resetParameters": {},
      "brainNames": ["RealFakeBrain"],
      "externalBrainNames": ["RealFakeBrain"],
      "logPath":"RealFakePath",
      "apiNumber":"API-3",
      "brainParameters": [{
          "vectorObservationSize": 3,
          "numStackedVectorObservations": 2,
          "vectorActionSize": 2,
          "memorySize": 0,
          "cameraResolutions": [],
          "vectorActionDescriptions": ["",""],
          "vectorActionSpaceType": 1,
          "vectorObservationSpaceType": 1
          }]
    }'''.encode()

    tf.reset_default_graph()
    with mock.patch('subprocess.Popen'):
        with mock.patch('socket.socket') as mock_socket:
            with mock.patch('glob.glob') as mock_glob:
                # End of mock
                with tf.Session() as sess:
                    with tf.variable_scope("FakeGraphScope"):
                        mock_glob.return_value = ['FakeLaunchPath']
                        mock_socket.return_value.accept.return_value = (mock_socket, 0)
                        mock_socket.recv.return_value.decode.return_value = c_action_c_state_start
                        env = UnityEnvironment(' ')

                        model = PPOModel(env.brains["RealFakeBrain"])
                        init = tf.global_variables_initializer()
                        sess.run(init)

                        run_list = [model.output, model.probs, model.value, model.entropy,
                                    model.learning_rate]
                        feed_dict = {model.batch_size: 2,
                                     model.sequence_length: 1,
                                     model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                               [3, 4, 5, 3, 4, 5]])}
                        sess.run(run_list, feed_dict=feed_dict)
                        env.close()


def test_ppo_model_discrete():
    d_action_c_state_start = '''{
      "AcademyName": "RealFakeAcademy",
      "resetParameters": {},
      "brainNames": ["RealFakeBrain"],
      "externalBrainNames": ["RealFakeBrain"],
      "logPath":"RealFakePath",
      "apiNumber":"API-3",
      "brainParameters": [{
          "vectorObservationSize": 3,
          "numStackedVectorObservations": 2,
          "vectorActionSize": 2,
          "memorySize": 0,
          "cameraResolutions": [{"width":30,"height":40,"blackAndWhite":false}],
          "vectorActionDescriptions": ["",""],
          "vectorActionSpaceType": 0,
          "vectorObservationSpaceType": 1
          }]
    }'''.encode()

    tf.reset_default_graph()
    with mock.patch('subprocess.Popen'):
        with mock.patch('socket.socket') as mock_socket:
            with mock.patch('glob.glob') as mock_glob:
                # End of mock
                with tf.Session() as sess:
                    with tf.variable_scope("FakeGraphScope"):
                        mock_glob.return_value = ['FakeLaunchPath']
                        mock_socket.return_value.accept.return_value = (mock_socket, 0)
                        mock_socket.recv.return_value.decode.return_value = d_action_c_state_start
                        env = UnityEnvironment(' ')
                        model = PPOModel(env.brains["RealFakeBrain"])
                        init = tf.global_variables_initializer()
                        sess.run(init)

                        run_list = [model.output, model.all_probs, model.value, model.entropy,
                                    model.learning_rate]
                        feed_dict = {model.batch_size: 2,
                                     model.sequence_length: 1,
                                     model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                               [3, 4, 5, 3, 4, 5]]),
                                     model.visual_in[0]: np.ones([2, 40, 30, 3])
                                     }
                        sess.run(run_list, feed_dict=feed_dict)
                        env.close()
=======
from unitytrainers.ppo.trainer import discount_rewards
from unityagents import UnityEnvironment
from .mock_communicator import MockCommunicator


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_cc_vector(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=False, visual_inputs=0)
            env = UnityEnvironment(' ')

            model = PPOModel(env.brains["RealFakeBrain"])
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.probs, model.value, model.entropy,
                        model.learning_rate]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]])}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_cc_visual(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=False, visual_inputs=2)
            env = UnityEnvironment(' ')

            model = PPOModel(env.brains["RealFakeBrain"])
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.probs, model.value, model.entropy,
                        model.learning_rate]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]]),
                         model.visual_in[0]: np.ones([2, 40, 30, 3]),
                         model.visual_in[1]: np.ones([2, 40, 30, 3])}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_dc_visual(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=True, visual_inputs=2)
            env = UnityEnvironment(' ')
            model = PPOModel(env.brains["RealFakeBrain"])
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]]),
                         model.visual_in[0]: np.ones([2, 40, 30, 3]),
                         model.visual_in[1]: np.ones([2, 40, 30, 3])
                         }
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_dc_vector(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=True, visual_inputs=0)
            env = UnityEnvironment(' ')
            model = PPOModel(env.brains["RealFakeBrain"])
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]])}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_dc_vector_rnn(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=True, visual_inputs=0)
            env = UnityEnvironment(' ')
            memory_size = 128
            model = PPOModel(env.brains["RealFakeBrain"], use_recurrent=True, m_size=memory_size)
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate, model.memory_out]
            feed_dict = {model.batch_size: 1,
                         model.sequence_length: 2,
                         model.prev_action: [0, 0],
                         model.memory_in: np.zeros((1, memory_size)),
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]])}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_cc_vector_rnn(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=False, visual_inputs=0)
            env = UnityEnvironment(' ')
            memory_size = 128
            model = PPOModel(env.brains["RealFakeBrain"], use_recurrent=True, m_size=memory_size)
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate, model.memory_out]
            feed_dict = {model.batch_size: 1,
                         model.sequence_length: 2,
                         model.memory_in: np.zeros((1, memory_size)),
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]])}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_dc_vector_curio(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=True, visual_inputs=0)
            env = UnityEnvironment(' ')
            model = PPOModel(env.brains["RealFakeBrain"], use_curiosity=True)
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate, model.intrinsic_reward]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]]),
                         model.next_vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                         [3, 4, 5, 3, 4, 5]]),
                         model.action_holder: [0, 0]}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_cc_vector_curio(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=False, visual_inputs=0)
            env = UnityEnvironment(' ')
            model = PPOModel(env.brains["RealFakeBrain"], use_curiosity=True)
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate, model.intrinsic_reward]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]]),
                         model.next_vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                         [3, 4, 5, 3, 4, 5]]),
                         model.output: [[0.0, 0.0], [0.0, 0.0]]}
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_dc_visual_curio(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=True, visual_inputs=2)
            env = UnityEnvironment(' ')
            model = PPOModel(env.brains["RealFakeBrain"], use_curiosity=True)
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate, model.intrinsic_reward]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]]),
                         model.next_vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                         [3, 4, 5, 3, 4, 5]]),
                         model.action_holder: [0, 0],
                         model.visual_in[0]: np.ones([2, 40, 30, 3]),
                         model.visual_in[1]: np.ones([2, 40, 30, 3]),
                         model.next_visual_in[0]: np.ones([2, 40, 30, 3]),
                         model.next_visual_in[1]: np.ones([2, 40, 30, 3])
                         }
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


@mock.patch('unityagents.UnityEnvironment.executable_launcher')
@mock.patch('unityagents.UnityEnvironment.get_communicator')
def test_ppo_model_cc_visual_curio(mock_communicator, mock_launcher):
    tf.reset_default_graph()
    with tf.Session() as sess:
        with tf.variable_scope("FakeGraphScope"):
            mock_communicator.return_value = MockCommunicator(
                discrete_action=False, visual_inputs=2)
            env = UnityEnvironment(' ')
            model = PPOModel(env.brains["RealFakeBrain"], use_curiosity=True)
            init = tf.global_variables_initializer()
            sess.run(init)

            run_list = [model.output, model.all_probs, model.value, model.entropy,
                        model.learning_rate, model.intrinsic_reward]
            feed_dict = {model.batch_size: 2,
                         model.sequence_length: 1,
                         model.vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                    [3, 4, 5, 3, 4, 5]]),
                         model.next_vector_in: np.array([[1, 2, 3, 1, 2, 3],
                                                         [3, 4, 5, 3, 4, 5]]),
                         model.output: [[0.0, 0.0], [0.0, 0.0]],
                         model.visual_in[0]: np.ones([2, 40, 30, 3]),
                         model.visual_in[1]: np.ones([2, 40, 30, 3]),
                         model.next_visual_in[0]: np.ones([2, 40, 30, 3]),
                         model.next_visual_in[1]: np.ones([2, 40, 30, 3])
                         }
            sess.run(run_list, feed_dict=feed_dict)
            env.close()


def test_rl_functions():
    rewards = np.array([0.0, 0.0, 0.0, 1.0])
    gamma = 0.9
    returns = discount_rewards(rewards, gamma, 0.0)
    np.testing.assert_array_almost_equal(returns, np.array([0.729, 0.81, 0.9, 1.0]))
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712


if __name__ == '__main__':
    pytest.main()
