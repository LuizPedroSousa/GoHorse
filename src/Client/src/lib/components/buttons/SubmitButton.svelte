<script>
	import Spinner from '../Spinner.svelte';

	export let isLoading = false;
	export let className = '';
</script>

<button
	class="{className} h-16 w-full rounded-md bg-primary text-base font-semibold text-gray-500 hover:bg-gray-500 hover:text-white hover:opacity-50"
	type="submit"
>
	{#if isLoading}
		<Spinner />
	{:else}
		<slot />
	{/if}
</button>
