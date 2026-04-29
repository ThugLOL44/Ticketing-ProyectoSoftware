function createSeatButton(seat, onReserve) {
    const button = document.createElement('button');
    button.className = `seat ${getSeatClass(seat.status)}`;
    button.textContent = seat.seatNumber;
    button.title = `Fila ${seat.rowIdentifier} - Butaca ${seat.seatNumber} (${seat.status})`;
    button.dataset.seatId = seat.id;
    button.dataset.status = seat.status;

    if (seat.status !== 'Available') {
        button.disabled = true;
    } else {
        button.addEventListener('click', () => onReserve(button, seat));
    }

    return button;
}

function getSeatClass(status) {
    const classes = {
        'Available': 'seat-available',
        'Reserved':  'seat-reserved',
        'Sold':      'seat-sold'
    };
    return classes[status] ?? 'seat-sold';
}

function markSeatAsReserved(button) {
    button.className = 'seat seat-reserved';
    button.disabled = true;
    button.dataset.status = 'Reserved';
}