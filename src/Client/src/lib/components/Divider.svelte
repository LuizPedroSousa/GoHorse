<div class="my-2 flex w-full items-center justify-between text-lg font-semibold text-white">
	<div class="line">&nbsp;</div>
	<slot />
	<div class="line" />
	&nbsp;
</div>

<style lang="postcss">
	div > div {
		height: 1px;
		width: theme('width.full');
		background-color: theme('colors.gray.500');
	}
</style>
