<script lang="ts">
	import GoogleIcon from '$lib/components/icons/GoogleIcon.svelte';
	export let className = '';
	export let title: string;
</script>

<a
	class="{className} flex h-16 w-full items-center justify-center rounded-md bg-white text-base font-semibold text-gray-500 hover:bg-gray-600 hover:no-underline "
	href="/google"
>
	<span class="mr-2 h-6 w-6">
		<GoogleIcon />
	</span>
	{title}
</a>
