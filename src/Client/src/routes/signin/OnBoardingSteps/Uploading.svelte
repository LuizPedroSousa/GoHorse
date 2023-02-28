<script>
	import SocialIllustration from '$lib/components/illustrations/SocialIllustration.svelte';
	import { fade } from 'svelte/transition';
</script>

<div class="px-20" in:fade>
	<h1 class="mt-1 max-w-lg text-left text-4xl font-medium text-white">
		Automated Syncing and Uploading Made Easy!
	</h1>
	<p class="mt-2">
		Streamlines your social media presence by automating the syncing and uploading process. With
		just a few clicks, you can easily manage your content across multiple platforms, saving you time
		and effort.
	</p>
	<span class="mt-6 flex w-full items-center justify-center">
		<SocialIllustration />
	</span>
</div>

<style>
	p {
		max-width: 70%;
	}
</style>
