<script lang="ts">
	import { type SvelteComponent, onMount } from 'svelte';
	import LambdaIcon from '../icons/LambdaIcon.svelte';
	import onnBoardingImg from '$lib/images/onboarding-background.png';

	interface Step {
		component: typeof SvelteComponent;
		props?: Record<string, any>;
	}

	export let className = '';
	export let steps: Step[];

	let currentStep = 0;
	let canGoNext = true;
	let autoNextTimeout: NodeJS.Timeout;

	function setCurrentStep(step: number) {
		canGoNext = false;
		currentStep = step;

		clearTimeout(autoNextTimeout);

		autoNextTimeout = setTimeout(() => {
			canGoNext = true;
			autoNext();
		}, 12000);
	}

	function autoNext() {
		if (!canGoNext) return;

		const getNextStep = () => {
			return currentStep === steps.length - 1 ? 0 : currentStep + 1;
		};

		currentStep = getNextStep();

		setTimeout(() => {
			autoNext();
		}, 10000);
	}

	onMount(autoNext);
</script>

<div
	class="relative flex h-full w-full flex-col items-start justify-between overflow-hidden rounded-xl {className}"
>
	<img class="-full absolute z-10 w-full rounded-2xl" src={onnBoardingImg} alt="Splash" />
	<div class="z-20 ml-20 mt-14 flex flex-col">
		<LambdaIcon className="w-14 h-14" />
		<h2 class="mt-10 text-lg font-semibold text-white">Go Horse</h2>
	</div>

	<div class="z-20 flex h-full w-full flex-col justify-start">
		<svelte:component this={steps[currentStep].component} {...steps[currentStep].props} />

		<div class="bottom-0 mx-auto mt-auto flex">
			{#each steps as _, i}
				<button
					class="{i === currentStep ? 'w-12' : 'w-4'} mr-3 h-4 rounded-{i === currentStep
						? 'md'
						: 'full'} bg-pink-300 opacity-{i === currentStep ? '100' : '50'} hover:opacity-20"
					on:click={() => setCurrentStep(i)}
					type="button"
				>
					&nbsp;
				</button>
			{/each}
		</div>
	</div>

	<div class="z-20 ml-20 mb-14 mt-10 flex items-center">
		<a href="/policy">privace policy</a>
		<span class="mx-2 h-2 w-2 rounded-full bg-white" />
		<a href="/terms">terms of service</a>
	</div>
</div>

<style lang="postcss">
	a {
		font-weight: theme('fontWeight.semibold');
		font-size: theme('fontSize.base');
		color: theme('colors.white');
		text-transform: uppercase;
	}
</style>
