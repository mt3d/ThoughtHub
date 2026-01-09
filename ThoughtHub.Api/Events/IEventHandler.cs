namespace ThoughtHub.Events
{
	/**
	 * The type parameter is contravariant. You can use a less derived type
	 * than the specified by the generic parameter.
	 * 
	 * A type can be declared contravariant in a generic interface or delegate
	 * only if it defines the type of a method's parameters and not of a method's return type.
	 * 
	 * istr = iobj
	 */
	public interface IEventHandler<in TEvent>
	{
		Task HandleAsync(TEvent e, CancellationToken ct);
	}
}
