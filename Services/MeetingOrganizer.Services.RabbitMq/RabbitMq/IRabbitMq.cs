namespace MeetingOrganizer.Services.RabbitMq;

/// <summary>
/// Represents a RabbitMQ service for interacting with RabbitMQ messaging.
/// </summary>
public delegate Task OnDataReceiveEvent<T>(T data);

/// <summary>
/// Interface for interacting with RabbitMQ messaging.
/// </summary>
public interface IRabbitMq
{
    /// <summary>
    /// Subscribes to a RabbitMQ queue for receiving messages of type T.
    /// </summary>
    /// <typeparam name="T">The type of data to receive.</typeparam>
    /// <param name="queueName">The name of the queue to subscribe to.</param>
    /// <param name="onReceive">The callback to execute when a message is received.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Subscribe<T>(string queueName, OnDataReceiveEvent<T> onReceive);

    /// <summary>
    /// Pushes data of type T to a RabbitMQ queue.
    /// </summary>
    /// <typeparam name="T">The type of data to push.</typeparam>
    /// <param name="queueName">The name of the queue to push data to.</param>
    /// <param name="data">The data to push to the queue.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PushAsync<T>(string queueName, T data);
}
