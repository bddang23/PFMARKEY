

function NumAnimation() {
	var STATUS_START = 1,
		STATUS_MIDDLE = 2,
		STATUS_END = 3,
		TIMEOUT = 50,
		TICKS = 5,
		el = document.getElementById("num"),
		value = el.dataset.value,
		target = [],
		dest = [],
		status = STATUS_START,
		ticks = 0,
		index = 0,
		random, handler, intervalId;

	random = function () {
		return Math.round(1 + Math.random() * 8);
	};

	handler = function () {
		switch (status) {
			case STATUS_START:
				for (var i = 0; i <= index; i += 1) {
					if (typeof target[i] === "number") {
						dest[i] = random();
					} else {
						dest[i] = target[i];
					}
				}
				break;

			case STATUS_MIDDLE:
				break;

			case STATUS_END:
				for (var i = index; i < target.length; i += 1) {
					if (typeof target[i] === "number") {
						if (i === index && ticks === TICKS - 1) {
							dest[i] = target[i];
						} else {
							dest[i] = random();
						}
					}
				}
				break;
		}

		ticks += 1;

		if (ticks >= TICKS) {
			ticks = 0;

			index += 1;

			if (index >= target.length) {
				index = 0;

				switch (status) {
					case STATUS_START:
						status = STATUS_END; //STATUS_MIDDLE;
						break;

					case STATUS_MIDDLE:
						status = STATUS_END;
						break;

					case STATUS_END:
						clearInterval(intervalId);
						break;
				}
			}
		}

		el.innerText = dest.join("");
	};

	value.split("").forEach(function (part, index) {
		var int = parseInt(part);
		target[index] = isNaN(int) ? part : int;
	});

	intervalId = setInterval(handler, TIMEOUT);
};
function NumAnimation2() {
	var STATUS_START = 1,
		STATUS_MIDDLE = 2,
		STATUS_END = 3,
		TIMEOUT = 50,
		TICKS = 5,
		el = document.getElementById("num2"),
		value = el.dataset.value,
		target = [],
		dest = [],
		status = STATUS_START,
		ticks = 0,
		index = 0,
		random, handler, intervalId;


	random = function () {
		return Math.round(1 + Math.random() * 8);
	};

	handler = function () {
		switch (status) {
			case STATUS_START:
				for (var i = 0; i <= index; i += 1) {
					if (typeof target[i] === "number") {
						dest[i] = random();
					} else {
						dest[i] = target[i];
					}
				}
				break;

			case STATUS_MIDDLE:
				break;

			case STATUS_END:
				for (var i = index; i < target.length; i += 1) {
					if (typeof target[i] === "number") {
						if (i === index && ticks === TICKS - 1) {
							dest[i] = target[i];
						} else {
							dest[i] = random();
						}
					}
				}
				break;
		}

		ticks += 1;

		if (ticks >= TICKS) {
			ticks = 0;

			index += 1;

			if (index >= target.length) {
				index = 0;

				switch (status) {
					case STATUS_START:
						status = STATUS_END; //STATUS_MIDDLE;
						break;

					case STATUS_MIDDLE:
						status = STATUS_END;
						break;

					case STATUS_END:
						clearInterval(intervalId);
						break;
				}
			}
		}

		el.innerText = dest.join("");
	};

	value.split("").forEach(function (part, index) {
		var int = parseInt(part);
		target[index] = isNaN(int) ? part : int;
	});

	intervalId = setInterval(handler, TIMEOUT);
};

function loopFunction(delay, callback, callback2) {
	var loop = function () {
		callback();
		callback2();
		setTimeout(loop, delay);
	}; loop();
};

loopFunction(15000, NumAnimation, NumAnimation2);

