<script>
	import WelcomeIllustration from '$lib/components/illustrations/WelcomeIllustration.svelte';

	import { fade } from 'svelte/transition';
</script>

<div class="px-20" in:fade>
	<h1 class="mt-1 max-w-lg text-left text-4xl font-medium text-white">Welcome to GoHorse!</h1>

	<p class="mt-2 text-base">
		We're excited to have you on board! With GoHorse, you'll be able to manage your social media
		accounts like a pro.<br /> So, let's get started - and thanks for choosing GoHorse!
	</p>

	<span class="mt-20 flex w-full items-center justify-center">
		<WelcomeIllustration />
	</span>
</div>

<style>
	p {
		max-width: 70%;
	}
</style>
