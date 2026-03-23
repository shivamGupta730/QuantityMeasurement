// UC16 Fixed - Pure Backend API + DB Storage (No Local Fallback)
const API_BASE = 'http://localhost:5250/api/Measurement';

// ================= COMPLETE UNITS =================
const units = {
  length: ['FEET', 'INCHES', 'YARDS', 'CENTIMETERS'],
  volume: ['LITRE', 'MILLILITRE', 'GALLON'],
  weight: ['KILOGRAM', 'GRAM'],
  temperature: ['CELSIUS', 'FAHRENHEIT', 'KELVIN']
};

const baseUnits = {
  length: 'FEET',
  volume: 'LITRE',
  weight: 'KILOGRAM',
  temperature: 'CELSIUS'
};

// ================= GLOBAL STATE =================
let currentType = 'length';
let currentAction = 'convert';

// ================= DOM READY =================
document.addEventListener('DOMContentLoaded', () => {
  populateUnits();
  toggleActionButtons();
  bindEvents();
});

function bindEvents() {
  // Type selection
  document.querySelectorAll('.type-card').forEach(card => {
    card.addEventListener('click', () => {
      document.querySelectorAll('.type-card').forEach(c => c.classList.remove('active'));
      card.classList.add('active');
      currentType = card.dataset.type;
      populateUnits();
      toggleActionButtons();
      clearAll();
    });
  });

  // Action buttons
  document.querySelectorAll('.action-btn').forEach(btn => {
    btn.addEventListener('click', () => switchAction(btn.dataset.action));
  });
}

function switchAction(action) {
  currentAction = action;
  document.querySelectorAll('.action-btn').forEach(btn => btn.classList.toggle('active', btn.dataset.action === action));
  document.querySelectorAll('.form-section').forEach(sec => sec.classList.toggle('active', sec.id === action + '-form'));
  clearAll();
}

function toggleActionButtons() {
  const isTemp = currentType === 'temperature';
  document.querySelectorAll('.action-btn[data-action="add"], .action-btn[data-action="subtract"]').forEach(btn => {
    btn.style.display = isTemp ? 'none' : 'block';
  });
  if (isTemp && currentAction !== 'convert') switchAction('convert');
}

function populateUnits() {
  const unitList = units[currentType];
  const ids = [
    'convert-from-unit', 'convert-to-unit',
    'add-unit1', 'add-unit2', 'add-target-unit',
    'subtract-unit1', 'subtract-unit2', 'subtract-target-unit',
    'compare-unit1', 'compare-unit2'
  ];
  
  ids.forEach(id => {
    const el = document.getElementById(id);
    if (el) {
      el.innerHTML = unitList.map(u => `<option value="${u}">${u}</option>`).join('');
    }
  });
}

function clearAll() {
  document.querySelectorAll('input[type="number"]').forEach(input => input.value = '');
  document.getElementById('main-result-box')?.classList.add('hidden');
  document.getElementById('error')?.classList.add('hidden');
}

function showLoading(show = true) {
  document.getElementById('loading').classList.toggle('hidden', !show);
}

function showError(msg) {
  const errorEl = document.getElementById('error');
  errorEl.textContent = msg;
  errorEl.classList.remove('hidden');
  showLoading(false);
}

// ================= MAIN CALCULATE =================
window.calculate = async () => {
  showLoading(true);
  const resultBox = document.getElementById('main-result-box');
  
  try {
    let endpoint;
    let body;

    if (currentAction === 'convert') {
      const value = parseFloat(document.getElementById('convert-from-value').value);
      const fromUnit = document.getElementById('convert-from-unit').value;
      const toUnit = document.getElementById('convert-to-unit').value;
      
      if (isNaN(value)) throw new Error('Enter valid number');
      
      endpoint = `convert-${currentType}`;
      body = { value, sourceUnit: fromUnit, targetUnit: toUnit };
      
    } else if (currentAction === 'compare') {
      const value1 = parseFloat(document.getElementById('compare-value1').value);
      const value2 = parseFloat(document.getElementById('compare-value2').value);
      const unit1 = document.getElementById('compare-unit1').value;
      const unit2 = document.getElementById('compare-unit2').value;
      
      if (isNaN(value1) || isNaN(value2)) throw new Error('Enter valid numbers');
      
      endpoint = `compare-${currentType}`;
      body = { value1, value2, unit1, unit2 };
      
    } else {
      // Add/Subtract
      const prefix = currentAction;
      const value1 = parseFloat(document.getElementById(prefix + '-value1').value);
      const value2 = parseFloat(document.getElementById(prefix + '-value2').value);
      const unit1 = document.getElementById(prefix + '-unit1').value;
      const unit2 = document.getElementById(prefix + '-unit2').value;
      const targetUnit = document.getElementById(prefix + '-target-unit').value;
      
      if (isNaN(value1) || isNaN(value2)) throw new Error('Enter valid numbers');
      
      endpoint = `${currentAction === 'add' ? 'add' : 'subtract'}-${currentType}s`;
      body = { value1, value2, unit1, unit2, targetUnit };
    }

    const response = await fetch(`${API_BASE}/${endpoint}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    });

    if (!response.ok) throw new Error(`Server error: ${response.status}`);
    
    const data = await response.json();
    resultBox.textContent = `Result: ${data.value?.toFixed(4) || data.message} ${data.unit || ''}`;
    console.log('✅ Backend success:', data);
    
  } catch (error) {
    showError(`Error: ${error.message}. Backend API must be running.`);
    console.error('API call failed:', error);
  } finally {
    resultBox.classList.remove('hidden');
    showLoading(false);
  }
};
