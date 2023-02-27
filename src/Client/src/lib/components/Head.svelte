<script lang="ts">
	import { PUBLIC_BASE_URL } from '$env/static/public';

	export let title: string;
	export let description: string;
	export let thumb: string;
	let path = '';
</script>

<svelte:head>
	<meta name="description" content="Svelte demo app" />
	<title>{title} | GoHorse</title>
	<meta name="title" content="{title} | GoHorse" />
	<meta name="description" content={description} />
	<meta name="theme-color" content={'#333'} />
	<meta name="image" content="{PUBLIC_BASE_URL}/images/thumbs${thumb}" />
	<meta property="image:alt" content={`${title} | GoHorse`} />

	<meta property="og:title" content={`${title} | GoHorse`} />
	<meta property="og:locale" content="pt_BR" />
	<meta property="og:site_name" content="GoHorse" />
	<meta property="og:image:width" content="1200" />
	<meta property="og:image:width" content="627" />
	<meta property="og:type" content="website" />
	<meta property="og:url" content="{PUBLIC_BASE_URL}/{path}" />
	<meta property="og:description" content={description} />
	<meta property="og:image" content="{PUBLIC_BASE_URL}/images/thumbs{thumb}" />
	<meta property="og:image:type" content="image/png" />
	<meta property="og:image:alt" content="{title} | GoHorse" />

	<meta name="twitter:title" content="{title} | GoHorse" />
	<meta name="twitter:card" content="summary_large_image" />
	<meta name="twitter:url" content="{PUBLIC_BASE_URL}/{path}" />
	<meta name="twitter:description" content={description} />
	<meta name="twitter:image" content="{PUBLIC_BASE_URL}/images/thumbs${thumb}" />
	<meta name="twitter:image:src" content="{PUBLIC_BASE_URL}/images/thumbs${thumb}" />
	<meta name="twitter:image:alt" content={'{title} | GoHorse'} />
	<meta name="twitter:image:width" content="1200" />
	<meta name="twitter:image:width" content="627" />
</svelte:head>
