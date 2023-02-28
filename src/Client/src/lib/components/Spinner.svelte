<script lang="ts">
	import { fade } from 'svelte/transition';

	let pause = false;
	let duration = '1.5s';
</script>

<div class="relative z-10 h-full w-full" in:fade={{ duration: 150 }}>
	<div class="absolute z-10 h-full w-full bg-white opacity-50 dark:bg-cyan-800" />
	<div class="absolute z-20 flex h-full w-full items-center justify-center">
		<div class="flex h-2 items-center justify-center leading-none" style="--duration: {duration};">
			<div class="inner">
				<div class="ball-container" class:pause-animation={pause}>
					<div class="single-ball">
						<div class="ball top-0 left-0 bg-red-500" class:pause-animation={pause}>&nbsp;</div>
					</div>
					<div class="contener_mixte">
						<div
							class="ball left- ball-top-right top-0 bg-yellow-500"
							class:pause-animation={pause}
						>
							&nbsp;
						</div>
					</div>
					<div class="contener_mixte">
						<div class="ball left-0" class:pause-animation={pause}>&nbsp;</div>
					</div>
					<div class="contener_mixte">
						<div class="ball" class:pause-animation={pause}>&nbsp;</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<style>
	.inner {
		transform: scale(calc(58 / 52));
	}
	.ball-container {
		animation: ballTwo var(--duration) infinite;
		width: 44px;
		height: 44px;
		flex-shrink: 0;
		position: relative;
	}
	.single-ball {
		width: 44px;
		height: 44px;
		position: absolute;
	}
	.ball {
		width: 20px;
		height: 20px;
		border-radius: 50%;
		position: absolute;
		left: 24px;
		top: 24px;
		animation: ballOne var(--duration) infinite ease;
	}
	.pause-animation {
		animation-play-state: paused;
	}
	@keyframes ballOne {
		0% {
			position: absolute;
		}
		50% {
			top: 12px;
			left: 12px;
			position: absolute;
			opacity: 0.5;
		}
		100% {
			position: absolute;
		}
	}
	@keyframes ballTwo {
		0% {
			transform: rotate(0deg) scale(1);
		}
		50% {
			transform: rotate(360deg) scale(1.3);
		}
		100% {
			transform: rotate(720deg) scale(1);
		}
	}
</style>
