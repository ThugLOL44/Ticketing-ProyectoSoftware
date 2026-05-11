const USER_ID = '44444444-4444-4444-4444-444444444444';
const EVENT_ID = getEventIdFromUrl();
let activeReservations = [];
let countdownInterval = null;

function getEventIdFromUrl() {
    const params = new URLSearchParams(window.location.search);
    return params.get('eventId') ?? '11111111-1111-1111-1111-111111111111';
}

function showToast(message, type = 'success') {
    const container = document.getElementById('toastContainer');
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.textContent = message;
    container.appendChild(toast);
    setTimeout(() => {
        toast.classList.add('toast-fade-out');
        setTimeout(() => toast.remove(), 400);
    }, 3500);
}

async function handleSeatClick(button, seat) {
    button.disabled = true;
    button.textContent = '...';

const response = await fetch(`${API_BASE_URL}/api/v1/reservations`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ seatId: seat.id, userId: USER_ID })
    });

    if (response.ok) {
        const datos = await response.json();
        activeReservations.push({
            reservationId: datos.id,
            seatLabel: `${seat.rowIdentifier}${seat.seatNumber}`,
            price: seat.sectorPrice
        });
        renderCart();
        startCountdown();
        markSeatAsReserved(button);
        button.textContent = seat.seatNumber;
        showToast(`Butaca ${seat.rowIdentifier}${seat.seatNumber} reservada exitosamente`, 'success');
        return;
    }

    button.disabled = false;
    button.className = 'seat seat-available';
    button.textContent = seat.seatNumber;

    if (response.status === 409) {
        showToast('Otra persona reservó ese asiento justo antes. El mapa fue actualizado.', 'error');
        await loadSeatMap();
        return;
    }

    showToast('No se pudo reservar. Intentá de nuevo.', 'error');
}

function groupSeatsBySectorAndRow(seats) {
    const sectors = {};

    seats.forEach(seat => {
        if (!sectors[seat.sectorId]) {
            sectors[seat.sectorId] = {
                name: seat.sectorName,
                price: seat.sectorPrice,
                rows: {}
            };
        }

        const row = seat.rowIdentifier;

        if (!sectors[seat.sectorId].rows[row]) {
            sectors[seat.sectorId].rows[row] = [];
        }

        sectors[seat.sectorId].rows[row].push(seat);
    });

    return sectors;
}

function renderSeatMap(seats) {
    const seatMap = document.getElementById('seatMap');
    seatMap.innerHTML = '';

    const sectors = groupSeatsBySectorAndRow(seats);


    const ordered = Object.values(sectors).sort((a, b) => {
        if (a.name === 'VIP') return -1;
        if (b.name === 'VIP') return 1;
        return 0;
    });

    ordered.forEach(sector => {

        const sectorBlock = document.createElement('div');


        const type = sector.name.toLowerCase() === 'vip' ? 'vip' : 'general';
        sectorBlock.className = `sector-block ${type}`;

        sectorBlock.innerHTML = `
            <div class="sector-header">
                <div class="sector-name">${sector.name}</div>
                <div class="sector-price">$${sector.price.toLocaleString('es-AR')}</div>
            </div>
        `;

        Object.entries(sector.rows).forEach(([rowLabel, rowSeats]) => {
            const rowDiv = document.createElement('div');
            rowDiv.className = 'sector-row';

          
            const labelSpan = document.createElement('span');
            labelSpan.className = 'row-label';
            labelSpan.textContent = rowLabel;

            const seatsDiv = document.createElement('div');
            seatsDiv.className = 'seats-row';

            rowSeats.forEach(seat => {
                const btn = createSeatButton(seat, handleSeatClick);
                seatsDiv.appendChild(btn);
            });

            rowDiv.appendChild(labelSpan);
            rowDiv.appendChild(seatsDiv);
            sectorBlock.appendChild(rowDiv);
        });

        seatMap.appendChild(sectorBlock);
    });
}

async function loadSeatMap() {
    try {
        const seats = await getSeats(EVENT_ID);
        renderSeatMap(seats);

        document.getElementById('spinner').classList.add('hidden');
        document.getElementById('seatMap').classList.remove('hidden');
    } catch(err) {
        console.error('>>> loadSeatMap error:', err);
        document.getElementById('spinner').innerHTML =
            `<p style="color:red">Error al cargar</p>`;
    }
}

function onTimerExpired() {
    document.getElementById('cartPanel').classList.add('hidden');
    showToast('Tu reserva expiró — la butaca fue liberada automáticamente.', 'warning');
    loadSeatMap();
    activeReservations = [];
    countdownInterval = null;
}

function renderCart(){
    const listaReservas = document.getElementById('cartItems');
    document.getElementById('cartItems').innerHTML = "";

    let total = 0;
    activeReservations.forEach(reserva => {
        const itemReserva = document.createElement('div');
        itemReserva.className = 'cart-item';

        itemReserva.innerHTML = `
      <span class="seat">Asiento: ${reserva.seatLabel}</span>
      <span class="price">Precio: $${reserva.price}</span>
    `;

    total += reserva.price;

    listaReservas.appendChild(itemReserva);   
    });
    document.getElementById('cartTotal').innerHTML = `
      <span class="">Total a pagar: ${total}</span>    
      `;
    
    document.getElementById('cartPanel').classList.remove('hidden');
}

function startCountdown() {
    if (countdownInterval !== null) return;

    let segundos = 5 * 60;

    countdownInterval = setInterval(() => {
        segundos--;

        const minutos = Math.floor(segundos / 60);
        const segsRestantes = segundos % 60;

        document.getElementById('countdownTimer').textContent = 
            `${String(minutos).padStart(2, '0')}:${String(segsRestantes).padStart(2, '0')}`;

        if (segundos <= 0) {
            clearInterval(countdownInterval);
            countdownInterval = null;
            onTimerExpired();
        }
    }, 1000);
}

async function confirmPayment() {
    const btn = document.getElementById('confirmPaymentBtn');
    btn.disabled = true;
    btn.textContent = 'Procesando...';

    try {
        for (const reserva of activeReservations) {
            await processPayment(reserva.reservationId);
        }

        showToast('¡Compra exitosa! Tu entrada fue procesada correctamente.', 'success');
        clearInterval(countdownInterval);
        countdownInterval = null;
        activeReservations = [];
        document.getElementById('cartPanel').classList.add('hidden');
        loadSeatMap();

    } catch {
        showToast('Error al procesar el pago. Intentá nuevamente.', 'error');
        btn.disabled = false;
        btn.textContent = 'Confirmar pago';
    }
}
document.addEventListener('DOMContentLoaded', loadSeatMap);