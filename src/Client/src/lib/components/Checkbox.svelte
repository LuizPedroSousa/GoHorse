<script lang="ts">
	import CheckIcon from './icons/CheckIcon.svelte';

	export let label = '';
	export let className = '';

	export let isChecked: boolean;

	export let onChange: () => void;
</script>

<div class="{className} flex items-center">
	<button
		class="rouded-md mr-3 flex h-5 w-5 items-center justify-center hover:opacity-50 bg-{isChecked
			? 'primary'
			: 'gray-500'} {isChecked ? 'rounded-lg' : ''}"
		id={label}
		type="button"
		on:click={onChange}
	>
		{#if isChecked}
			<span class="text-white">
				<CheckIcon />
			</span>
		{/if}
	</button>

	{#if label}
		<label class="cursor-pointer text-base font-semibold text-white hover:opacity-50" for={label}
			>{label}</label
		>
	{/if}
</div>
