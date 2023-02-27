<script lang="ts">
	import EyeClosedIcon from './icons/EyeClosedIcon.svelte';
	import EyeIcon from './icons/EyeIcon.svelte';

	export let name: string;
	export let label: string;
	export let type: 'text' | 'password' = 'text';
	export let className = '';
	export let placeholder = label;

	let isVisivle = type === 'text';

	const toggleView = () => (isVisivle = !isVisivle);
</script>

<div class="{className} relative">
	<label class="text-base font-semibold text-white" for={name}>{label}</label>
	<slot />
	<div class="relative mt-2 h-16 w-full">
		<input
			{name}
			class="h-full w-full rounded-md bg-gray-500 px-4 text-white focus:outline-primary"
			type={isVisivle ? 'text' : 'password'}
			{placeholder}
		/>
		{#if type === 'password'}
			<button class="absolute right-6" type="button" on:click={toggleView}>
				<span class="flex h-6 w-6 items-center justify-center text-white">
					{#if isVisivle}
						<EyeClosedIcon />
					{:else}
						<EyeIcon />
					{/if}
				</span>
			</button>
		{/if}
	</div>
</div>

<style lang="postcss">
	button {
		top: 50%;
		transform: translateY(-50%);
	}
</style>
